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
    }
}
