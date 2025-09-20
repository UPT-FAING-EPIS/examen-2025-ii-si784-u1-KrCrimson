
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

app.Run();
