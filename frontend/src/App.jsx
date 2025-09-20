import React from 'react';
import FlightSearchForm from './components/FlightSearchForm';

function App() {
  return (
    <div style={{ maxWidth: 600, margin: '2rem auto', padding: '2rem', background: '#fff', borderRadius: 8, boxShadow: '0 2px 8px rgba(0,0,0,0.1)' }}>
      <h2>Sistema de Reserva de Vuelos</h2>
      <FlightSearchForm />
    </div>
  );
}

export default App;
