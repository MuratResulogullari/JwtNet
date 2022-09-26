import React from 'react';
import './App.css';
import { Routes, Route } from "react-router-dom";
import NotPage from '../common/NotPage';
import Preferences from '../Preferences/Preferences';
import DashBoard from '../Dashboard/DashBoard';
import Login from '../Login/Login';
import Home from '../home/Home';

function setToken(userToken) {
  sessionStorage.setItem('token', JSON.stringify(userToken));
}

function getToken() {
  const tokenString = sessionStorage.getItem('token');
  const userToken = JSON.parse(tokenString);
  return userToken?.token
}


function App() {
  const token = getToken();
  if (!token) {
    return <Login setToken={setToken} />
  }

  return (
    <>
      <div className="wrapper">
        <h1>Application</h1>
        <Routes>
          <Route exact path="/home" element={<Home />} />
          <Route exact path="/dashBoard" element={<DashBoard />} />
          <Route exact path="/preferences" element={<Preferences />} />
          <Route path="*" element={<NotPage />} />
        </Routes>
      </div>
    </>
  );
}

export default App;