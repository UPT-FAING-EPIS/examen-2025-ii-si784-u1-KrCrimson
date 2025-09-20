import React, { useState } from 'react';

export default function ReservasUsuario() {
  const [userId, setUserId] = useState('');
  const [reservas, setReservas] = useState([]);
  const [mensaje, setMensaje] = useState('');
  const [error, setError] = useState('');

  const consultarReservas = async (e) => {
    e.preventDefault();
    setMensaje('');
    if (!userId) {
      setError('Debe ingresar el ID de usuario.');
      return;
    }
    setError('');
    try {
      const res = await fetch(`http://localhost:5197/reservations/${userId}`);
      const data = await res.json();
      setReservas(data);
    } catch {
      setReservas([]);
      setMensaje('Error al consultar reservas.');
    }
  };

  const cancelarReserva = async (id) => {
    const res = await fetch(`http://localhost:5197/reservations/${id}`, { method: 'DELETE' });
    if (res.ok) {
      setMensaje('Reserva cancelada correctamente.');
      setReservas(reservas.filter(r => r.id !== id));
    } else {
      setMensaje('Error al cancelar reserva.');
    }
  };

  return (
    <div className="detalle">
      <h2>Mis Reservas</h2>
      <form onSubmit={consultarReservas} style={{ marginBottom: 10 }}>
        <input type="text" placeholder="ID Usuario" value={userId} onChange={e => setUserId(e.target.value)} required />
        <button type="submit">Consultar</button>
        {error && <div className="mensaje">{error}</div>}
      </form>
      {mensaje && <div className="mensaje">{mensaje}</div>}
      {reservas.length > 0 && (
        <table border="1" cellPadding="8" style={{ width: '100%' }}>
          <thead>
            <tr>
              <th>Vuelo</th>
              <th>Asiento</th>
              <th>Fecha</th>
              <th>Acción</th>
            </tr>
          </thead>
          <tbody>
            {reservas.map(r => (
              <tr key={r.id}>
                <td>{r.flightId}</td>
                <td>{r.seatNumber}</td>
                <td>{new Date(r.reservationDate).toLocaleString()}</td>
                <td><button onClick={() => cancelarReserva(r.id)}>Cancelar</button></td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}