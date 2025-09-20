using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure
{
    /// <summary>
    /// Contexto de base de datos para el sistema de reservas de vuelos.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales si son necesarias
        }
    }
}
