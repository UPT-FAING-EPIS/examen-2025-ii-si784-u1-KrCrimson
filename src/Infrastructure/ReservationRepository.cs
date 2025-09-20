using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure
{
    /// <summary>
    /// Repositorio concreto para la gestión de reservas usando Entity Framework Core.
    /// </summary>
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Reservations.Where(r => r.UserId == userId && !r.IsCancelled).ToListAsync();
        }

            public async Task<List<Reservation>> GetByFlightIdAsync(Guid flightId)
            {
                return await _context.Reservations.Where(r => r.FlightId == flightId && !r.IsCancelled).ToListAsync();
            }

        public async Task<Reservation?> GetByIdAsync(Guid id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
