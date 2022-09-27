import React from 'react';
import './App.css';
import { Routes, Route } from "react-router-dom";
import NotPage from '../common/NotPage';
import Preferences from '../preferences/Preferences';
import DashBoard from '../dashboard/DashBoard';
import Login from '../login/Login';
import Home from '../home/Home';

function setToken(userToken) {
  sessionStorage.setItem('token', JSON.stringify(userToken));
}
function getToken() {
  const tokenString = sessionStorage.getItem('token');
  const userToken = JSON.parse(tokenString);
  return userToken != null ? true : false;
}
function App() {
  const token = getToken();
  if (!token) {
    return <Login setToken={setToken} getToken={getToken} />
  }
  return (
    <>
      <div className="wrapper">
        <h1>Application</h1>
        <Routes>
          <Route exact path="/" element={<Home />} />
          <Route exact path="/dashboard" element={<DashBoard />} />
          <Route exact path="/preferences" element={<Preferences />} />
          <Route exact path="/login" element={<Login />} />
          <Route path="*" element={<NotPage />} />
        </Routes>
      </div>
    </>
  );
}

export default App;