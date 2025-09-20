import React, { useState } from 'react';
import './App.css';
import BusquedaVuelos from './BusquedaVuelos';
import ListadoVuelos from './ListadoVuelos';

import ReservasUsuario from './ReservasUsuario';
import DetalleVuelo from './DetalleVuelo';

function App() {
  const [vuelos, setVuelos] = useState([]);
  const [loading, setLoading] = useState(false);
  const [vueloSeleccionado, setVueloSeleccionado] = useState(null);

  // Función para buscar vuelos en el backend
  const buscarVuelos = async (filtros) => {
    setLoading(true);
    setVueloSeleccionado(null);
    try {
      // Construir query string
      const params = new URLSearchParams();
      if (filtros.origen) params.append('origin', filtros.origen);
      if (filtros.destino) params.append('destination', filtros.destino);
      if (filtros.fecha) params.append('date', filtros.fecha);
      // Llamada al backend
      const res = await fetch(`http://localhost:5197/flights?${params.toString()}`);
      const data = await res.json();
      setVuelos(data);
    } catch (err) {
      setVuelos([]);
    }
    setLoading(false);
  };

  // Función para refrescar vuelos tras reservar
  const refrescarVuelos = () => {
    setVueloSeleccionado(null);
    setVuelos([]);
  };

  return (
    <div className="App">
      <h1>Sistema de Reserva de Vuelos</h1>
      <BusquedaVuelos onBuscar={buscarVuelos} />
      {loading ? <p>Cargando...</p> : <ListadoVuelos vuelos={vuelos} onSeleccionar={setVueloSeleccionado} />}
      <DetalleVuelo vuelo={vueloSeleccionado} onReservar={refrescarVuelos} />
      <ReservasUsuario />
    </div>
  );
}

export default App;
