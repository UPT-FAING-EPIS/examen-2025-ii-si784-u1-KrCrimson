using System;

namespace Domain
{
    /// <summary>
    /// Representa un vuelo disponible en el sistema.
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// Identificador único del vuelo.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ciudad de origen del vuelo.
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Ciudad de destino del vuelo.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Fecha y hora de salida.
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Fecha y hora de llegada.
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Nombre de la aerolínea.
        /// </summary>
        public string Airline { get; set; }

        /// <summary>
        /// Precio del vuelo.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Asientos disponibles para reservar.
        /// </summary>
        public int AvailableSeats { get; set; }

        /// <summary>
        /// Total de asientos en el vuelo.
        /// </summary>
        public int TotalSeats { get; set; }

        /// <summary>
        /// Valida los datos del vuelo. Retorna un mensaje de error si hay datos inválidos, o null si todo es válido.
        /// </summary>
        public string? Validate()
        {
            if (string.IsNullOrWhiteSpace(Origin))
                return "El origen es obligatorio.";
            if (string.IsNullOrWhiteSpace(Destination))
                return "El destino es obligatorio.";
            if (DepartureTime >= ArrivalTime)
                return "La hora de llegada debe ser posterior a la de salida.";
            if (string.IsNullOrWhiteSpace(Airline))
                return "La aerolínea es obligatoria.";
            if (Price < 0)
                return "El precio no puede ser negativo.";
            if (TotalSeats <= 0)
                return "El vuelo debe tener al menos un asiento.";
            if (AvailableSeats < 0 || AvailableSeats > TotalSeats)
                return "El número de asientos disponibles no es válido.";
            return null;
        }
    }
}
