using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure
{
    /// <summary>
    /// Fábrica para crear AppDbContext en tiempo de diseño (migraciones).
    /// Permite a Entity Framework Core generar y aplicar migraciones correctamente.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // Cambia la contraseña por la real de tu usuario postgres si es necesario
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=reservas_vuelos;Username=postgres;Password=Sb27.11.22");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}