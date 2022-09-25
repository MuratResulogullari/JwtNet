import React, { useState } from 'react';
import './App.css';
import { Routes, Route } from "react-router-dom";
import NotPage from '../common/NotPage';
import Preferences from '../Preferences/Preferences';
import DashBoard from '../Dashboard/DashBoard';
import Login from '../Login/Login';

function App() {
  const [token, setToken] = useState();
  if (!token) {
    return <Login setToken={setToken} />
  }

  return (
    <>
      <div className="wrapper">
        <h1>Application</h1>
        <Routes>

          <Route exact path="/dashBoard" element={<DashBoard />} />
          <Route exact path="/preferences" element={<Preferences />} />
          <Route path="*" element={<NotPage />} />
        </Routes>
      </div>
    </>
  );
}

export default App;