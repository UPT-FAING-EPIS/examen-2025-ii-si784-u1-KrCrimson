using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure
{
    /// <summary>
    /// Interfaz para el repositorio de vuelos.
    /// </summary>
    public interface IFlightRepository
    {
        Task<List<Flight>> GetAllAsync();
        Task<Flight?> GetByIdAsync(Guid id);
        Task AddAsync(Flight flight);
        Task UpdateAsync(Flight flight);
        Task DeleteAsync(Guid id);
    }
}
