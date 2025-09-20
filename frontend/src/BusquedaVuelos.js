import React, { useState, useEffect } from 'react';

const API_URL = 'http://localhost:5197/flights';

export default function BusquedaVuelos({ onBuscar }) {
  const [origen, setOrigen] = useState('');
  const [destino, setDestino] = useState('');
  const [fecha, setFecha] = useState('');
  const [error, setError] = useState('');
  const [vuelos, setVuelos] = useState([]);
  const [origenes, setOrigenes] = useState([]);
  const [destinos, setDestinos] = useState([]);

  useEffect(() => {
    fetch(API_URL)
      .then(res => res.json())
      .then(data => {
        setVuelos(data);
        setOrigenes([...new Set(data.map(f => f.origin))]);
      });
  }, []);

  useEffect(() => {
    if (origen) {
      setDestinos([...new Set(vuelos.filter(f => f.origin === origen).map(f => f.destination))]);
    } else {
      setDestinos([]);
    }
    setDestino('');
  }, [origen, vuelos]);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!origen || !destino || !fecha) {
      setError('Todos los campos son obligatorios.');
      return;
    }
    setError('');
    onBuscar({ origen, destino, fecha });
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: 20, display: 'flex', gap: '1rem', alignItems: 'center' }}>
      <select value={origen} onChange={e => setOrigen(e.target.value)} required style={{ minWidth: 120 }}>
        <option value="">Origen</option>
        {origenes.map(o => (
          <option key={o} value={o}>{o}</option>
        ))}
      </select>
      <select value={destino} onChange={e => setDestino(e.target.value)} required disabled={!origen} style={{ minWidth: 120 }}>
        <option value="">Destino</option>
        {destinos.map(d => (
          <option key={d} value={d}>{d}</option>
        ))}
      </select>
      <input type="date" value={fecha} onChange={e => setFecha(e.target.value)} required style={{ minWidth: 140 }} />
      <button type="submit" style={{ padding: '0.5rem 1rem', background: '#2196f3', color: '#fff', border: 'none', borderRadius: 4 }}>Buscar vuelos</button>
      {error && <div className="mensaje">{error}</div>}
    </form>
  );
}