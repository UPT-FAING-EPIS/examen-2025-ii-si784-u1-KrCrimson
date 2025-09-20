using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    /// <summary>
    /// Servicio para la lógica de negocio relacionada con reservas de vuelos.
    /// </summary>
    public class ReservationService
    {
        private readonly List<Reservation> _reservations;
        private readonly List<Flight> _flights;

        /// <summary>
        /// Inicializa el servicio con listas de reservas y vuelos (simulación).
        /// </summary>
        public ReservationService(List<Reservation> reservations, List<Flight> flights)
        {
            _reservations = reservations;
            _flights = flights;
        }

        /// <summary>
        /// Crea una nueva reserva de asiento en un vuelo.
        /// </summary>
        /// <param name="flightId">ID del vuelo</param>
        /// <param name="userId">ID del usuario</param>
        /// <param name="seatNumber">Número de asiento</param>
        /// <returns>Reserva creada o mensaje de error</returns>
        public (Reservation? reservation, string? error) CreateReservation(Guid flightId, Guid userId, int seatNumber)
        {
            var flight = _flights.FirstOrDefault(f => f.Id == flightId);
            if (flight == null)
                return (null, "Vuelo no encontrado.");
            if (flight.AvailableSeats <= 0)
                return (null, "No hay asientos disponibles en este vuelo.");
            if (_reservations.Any(r => r.FlightId == flightId && r.SeatNumber == seatNumber && !r.IsCancelled))
                return (null, "El asiento ya está reservado.");
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                FlightId = flightId,
                UserId = userId,
                SeatNumber = seatNumber,
                ReservationDate = DateTime.UtcNow,
                IsCancelled = false
            };
            var validationError = reservation.Validate();
            if (validationError != null)
                return (null, validationError);
            _reservations.Add(reservation);
            flight.AvailableSeats--;
            return (reservation, null);
        }

        /// <summary>
        /// Lista las reservas de un usuario.
        /// </summary>
        public List<Reservation> GetReservationsByUser(Guid userId)
        {
            return _reservations.Where(r => r.UserId == userId && !r.IsCancelled).ToList();
        }

        /// <summary>
        /// Cancela una reserva por su ID.
        /// </summary>
        public string? CancelReservation(Guid reservationId)
        {
            var reservation = _reservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservation == null)
                return "Reserva no encontrada.";
            if (reservation.IsCancelled)
                return "La reserva ya está cancelada.";
            reservation.IsCancelled = true;
            var flight = _flights.FirstOrDefault(f => f.Id == reservation.FlightId);
            if (flight != null)
                flight.AvailableSeats++;
            return null;
        }
    }
}
