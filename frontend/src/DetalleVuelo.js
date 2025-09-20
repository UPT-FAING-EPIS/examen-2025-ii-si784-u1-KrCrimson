import React, { useState, useEffect } from 'react';


export default function DetalleVuelo({ vuelo, onReservar }) {
  const [seat, setSeat] = useState('');
  const [userId, setUserId] = useState('');
  const [mensaje, setMensaje] = useState('');
  const [error, setError] = useState('');
  const [ocupados, setOcupados] = useState([]);

  useEffect(() => {
    if (vuelo) {
      fetch(`http://localhost:5197/reservations/flight/${vuelo.id}`)
        .then(res => res.json())
        .then(data => setOcupados(data.map(r => r.seatNumber)));
    } else {
      setOcupados([]);
    }
  }, [vuelo]);

  if (!vuelo) return null;

  const handleReserva = async (e) => {
    e.preventDefault();
    const numSeat = parseInt(seat, 10);
    if (!seat || !userId || isNaN(numSeat) || numSeat <= 0) {
      setError('Debe ingresar un usuario y un número de asiento válido.');
      return;
    }
    if (ocupados.includes(numSeat)) {
      setError(`El asiento ${numSeat} ya está ocupado.`);
      return;
    }
    setError('');
    // Llamada al backend para reservar
    const res = await fetch('http://localhost:5197/reservations', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        flightId: vuelo.id,
        userId,
        seatNumber: numSeat
      })
    });
    if (res.ok) {
      setMensaje('Reserva realizada correctamente.');
      onReservar();
    } else {
      setMensaje('Error al reservar.');
    }
  };

  return (
    <div className="detalle">
      <h2>Detalle del Vuelo</h2>
      <p><b>Origen:</b> {vuelo.origin}</p>
      <p><b>Destino:</b> {vuelo.destination}</p>
      <p><b>Salida:</b> {new Date(vuelo.departureTime).toLocaleString()}</p>
      <p><b>Llegada:</b> {new Date(vuelo.arrivalTime).toLocaleString()}</p>
      <p><b>Aerolínea:</b> {vuelo.airline}</p>
      <div style={{ margin: '10px 0' }}>
        <b>Asientos ocupados:</b> {ocupados.length > 0 ? ocupados.join(', ') : 'Ninguno'}
      </div>
      <form onSubmit={handleReserva} style={{ marginTop: 10 }}>
        <input type="text" placeholder="ID Usuario" value={userId} onChange={e => setUserId(e.target.value)} required />
        <input type="number" placeholder="N° Asiento" value={seat} onChange={e => setSeat(e.target.value)} required min="1" />
        <button type="submit">Reservar</button>
        {error && <div className="mensaje">{error}</div>}
      </form>
      {mensaje && <div className="mensaje">{mensaje}</div>}
    </div>
  );
}