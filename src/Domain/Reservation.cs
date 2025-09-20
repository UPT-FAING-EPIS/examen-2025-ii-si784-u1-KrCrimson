using System;

namespace Domain
{
    /// <summary>
    /// Representa una reserva de asiento en un vuelo.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Identificador único de la reserva.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador del vuelo reservado.
        /// </summary>
        public Guid FlightId { get; set; }

        /// <summary>
        /// Identificador del usuario que realiza la reserva.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Número de asiento reservado.
        /// </summary>
        public int SeatNumber { get; set; }

        /// <summary>
        /// Fecha en que se realizó la reserva.
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Indica si la reserva fue cancelada.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}
