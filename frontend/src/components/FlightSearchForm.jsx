
import React, { useEffect, useState } from 'react';

const API_URL = 'http://localhost:5197/flights';

export default function FlightSearchForm() {
  const [flights, setFlights] = useState([]);
  const [origins, setOrigins] = useState([]);
  const [selectedOrigin, setSelectedOrigin] = useState('');
  const [selectedDestination, setSelectedDestination] = useState('');
  const [results, setResults] = useState([]);
  const [searched, setSearched] = useState(false);

  useEffect(() => {
    fetch(API_URL)
      .then(res => res.json())
      .then(data => {
        setFlights(data);
        setOrigins([...new Set(data.map(f => f.origin))]);
      });
  }, []);

  // Destinos válidos según el origen seleccionado
  const filteredDestinations = selectedOrigin
    ? [...new Set(flights.filter(f => f.origin === selectedOrigin).map(f => f.destination))]
    : [];


  const handleSearch = e => {
    e.preventDefault();
    setSearched(true);
    const filtered = flights.filter(f =>
      f.origin === selectedOrigin && f.destination === selectedDestination
    );
    setResults(filtered);
  };

  return (
    <form onSubmit={handleSearch} style={{ display: 'flex', flexDirection: 'column', gap: '1rem' }}>
      <label htmlFor="origin">Origen:</label>
      <select id="origin" value={selectedOrigin} onChange={e => {
        setSelectedOrigin(e.target.value);
        setSelectedDestination('');
        setResults([]);
        setSearched(false);
      }}>
        <option value="">Seleccione origen</option>
        {origins.map(origin => (
          <option key={origin} value={origin}>{origin}</option>
        ))}
      </select>

      <label htmlFor="destination">Destino:</label>
      <select id="destination" value={selectedDestination} onChange={e => setSelectedDestination(e.target.value)} disabled={!selectedOrigin}>
        <option value="">Seleccione destino</option>
        {filteredDestinations.map(destination => (
          <option key={destination} value={destination}>{destination}</option>
        ))}
      </select>

      <button type="submit" disabled={!selectedOrigin || !selectedDestination} style={{ padding: '0.5rem 1rem', background: '#1976d2', color: '#fff', border: 'none', borderRadius: 4, cursor: 'pointer' }}>
        Buscar vuelos
      </button>

      {searched && (
        <div style={{ marginTop: '2rem' }}>
          <h3>Resultados de búsqueda</h3>
          {results.length === 0 ? (
            <p>No hay vuelos disponibles para la selección.</p>
          ) : (
            <ul style={{ listStyle: 'none', padding: 0 }}>
              {results.map(flight => (
                <li key={flight.id} style={{ marginBottom: '1rem', padding: '1rem', background: '#f5f5f5', borderRadius: 6 }}>
                  <strong>{flight.airline}</strong> | {flight.origin} → {flight.destination}<br />
                  Salida: {new Date(flight.departureTime).toLocaleString()}<br />
                  Llegada: {new Date(flight.arrivalTime).toLocaleString()}<br />
                  Precio: S/ {flight.price}<br />
                  Asientos disponibles: {flight.availableSeats} / {flight.totalSeats}
                </li>
              ))}
            </ul>
          )}
        </div>
      )}
    </form>
  );
}
