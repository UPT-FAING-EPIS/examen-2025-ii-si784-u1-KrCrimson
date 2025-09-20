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

        /// <summary>
        /// Valida los datos de la reserva. Retorna un mensaje de error si hay datos inválidos, o null si todo es válido.
        /// </summary>
        public string? Validate()
        {
            if (FlightId == Guid.Empty)
                return "El vuelo es obligatorio.";
            if (UserId == Guid.Empty)
                return "El usuario es obligatorio.";
            if (SeatNumber <= 0)
                return "El número de asiento debe ser mayor a cero.";
            if (ReservationDate == default)
                return "La fecha de reserva es obligatoria.";
            return null;
        }
    }
}
