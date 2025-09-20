using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure
{
    /// <summary>
    /// Interfaz para el repositorio de reservas.
    /// </summary>
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetByUserIdAsync(Guid userId);
        Task<Reservation?> GetByIdAsync(Guid id);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Guid id);
    }
}
