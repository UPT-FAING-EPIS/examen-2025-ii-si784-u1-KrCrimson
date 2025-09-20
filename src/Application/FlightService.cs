using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    /// <summary>
    /// Servicio para la lógica de negocio relacionada con vuelos.
    /// </summary>
    public class FlightService
    {
        private readonly List<Flight> _flights;

        /// <summary>
        /// Inicializa el servicio con una lista de vuelos (simulación).
        /// </summary>
        public FlightService(List<Flight> flights)
        {
            _flights = flights;
        }

        /// <summary>
        /// Busca vuelos por origen, destino y fecha.
        /// </summary>
        /// <param name="origin">Ciudad de origen</param>
        /// <param name="destination">Ciudad de destino</param>
        /// <param name="date">Fecha de salida</param>
        /// <returns>Lista de vuelos que cumplen los criterios</returns>
        public List<Flight> SearchFlights(string origin, string destination, DateTime date)
        {
            return _flights.Where(f =>
                f.Origin.Equals(origin, StringComparison.OrdinalIgnoreCase) &&
                f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase) &&
                f.DepartureTime.Date == date.Date
            ).ToList();
        }
    }
}
