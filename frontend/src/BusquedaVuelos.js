import React, { useState } from 'react';

export default function BusquedaVuelos({ onBuscar }) {
  const [origen, setOrigen] = useState('');
  const [destino, setDestino] = useState('');
  const [fecha, setFecha] = useState('');
  const [error, setError] = useState('');

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
    <form onSubmit={handleSubmit} style={{ marginBottom: 20 }}>
      <input type="text" placeholder="Origen" value={origen} onChange={e => setOrigen(e.target.value)} required />
      <input type="text" placeholder="Destino" value={destino} onChange={e => setDestino(e.target.value)} required />
      <input type="date" value={fecha} onChange={e => setFecha(e.target.value)} required />
      <button type="submit">Buscar vuelos</button>
      {error && <div className="mensaje">{error}</div>}
    </form>
  );
}