import React from 'react';

/**
 * Componente para mostrar la lista de vuelos encontrados y seleccionar uno.
 */
export default function ListadoVuelos({ vuelos, onSeleccionar }) {
  if (!vuelos || vuelos.length === 0) {
    return <p>No se encontraron vuelos.</p>;
  }
  return (
    <table border="1" cellPadding="8" style={{ width: '100%', marginTop: 20 }}>
      <thead>
        <tr>
          <th>Origen</th>
          <th>Destino</th>
          <th>Salida</th>
          <th>Llegada</th>
          <th>Aerolínea</th>
          <th>Acción</th>
        </tr>
      </thead>
      <tbody>
        {vuelos.map(vuelo => (
          <tr key={vuelo.id}>
            <td>{vuelo.origin}</td>
            <td>{vuelo.destination}</td>
            <td>{new Date(vuelo.departureTime).toLocaleString()}</td>
            <td>{new Date(vuelo.arrivalTime).toLocaleString()}</td>
            <td>{vuelo.airline}</td>
            <td><button onClick={() => onSeleccionar(vuelo)}>Ver / Reservar</button></td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}