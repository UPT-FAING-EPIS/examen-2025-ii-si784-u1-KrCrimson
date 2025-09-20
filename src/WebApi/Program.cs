
using Infrastructure;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configuración de DbContext para PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro del repositorio de vuelos en el contenedor de dependencias
builder.Services.AddScoped<IFlightRepository, FlightRepository>();

// Registro del repositorio de reservas en el contenedor de dependencias
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

/// <summary>
/// Endpoint para obtener la lista de vuelos desde la base de datos PostgreSQL.
/// </summary>
app.MapGet("/flights", async (IFlightRepository flightRepo) =>
{
    // Consulta todos los vuelos usando el repositorio
    var flights = await flightRepo.GetAllAsync();
    return Results.Ok(flights);
})
.WithName("GetFlights");

/// <summary>
/// Endpoint para obtener los detalles de un vuelo por su ID.
/// </summary>
app.MapGet("/flights/{id}", async (Guid id, IFlightRepository flightRepo) =>
{
    var flight = await flightRepo.GetByIdAsync(id);
    if (flight == null)
        return Results.NotFound($"No se encontró el vuelo con ID {id}");
    return Results.Ok(flight);
})
.WithName("GetFlightById");

/// <summary>
/// Endpoint para registrar un vuelo en la base de datos (uso de prueba).
/// </summary>
app.MapPost("/flights", async (Flight flight, IFlightRepository flightRepo) =>
{
    // Validación básica
    if (string.IsNullOrWhiteSpace(flight.Origin) || string.IsNullOrWhiteSpace(flight.Destination))
        return Results.BadRequest("Origen y destino son obligatorios.");
    flight.Id = Guid.NewGuid();
    await flightRepo.AddAsync(flight);
    return Results.Created($"/flights/{flight.Id}", flight);
})
.WithName("CreateFlight");

app.Run();

/// <summary>
/// Endpoint para crear una nueva reserva.
/// </summary>
app.MapPost("/reservations", async (Reservation reservation, IReservationRepository reservationRepo) =>
{
    reservation.Id = Guid.NewGuid();
    reservation.ReservationDate = DateTime.UtcNow;
    reservation.IsCancelled = false;
    await reservationRepo.AddAsync(reservation);
    return Results.Created($"/reservations/{reservation.Id}", reservation);
})
.WithName("CreateReservation");

/// <summary>
/// Endpoint para listar reservas de un usuario.
/// </summary>
app.MapGet("/reservations/{userId}", async (Guid userId, IReservationRepository reservationRepo) =>
{
    var reservations = await reservationRepo.GetByUserIdAsync(userId);
    return Results.Ok(reservations);
})
.WithName("GetReservationsByUser");

/// <summary>
/// Endpoint para cancelar una reserva por su ID.
/// </summary>
app.MapDelete("/reservations/{id}", async (Guid id, IReservationRepository reservationRepo) =>
{
    var reservation = await reservationRepo.GetByIdAsync(id);
    if (reservation == null)
        return Results.NotFound($"No se encontró la reserva con ID {id}");
    reservation.IsCancelled = true;
    await reservationRepo.UpdateAsync(reservation);
    return Results.Ok($"Reserva {id} cancelada correctamente.");
})
.WithName("CancelReservation");
