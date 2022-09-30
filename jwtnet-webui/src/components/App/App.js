import React from 'react';
import './App.css';
import { Routes, Route } from "react-router-dom";
import NotPage from '../common/NotPage';
import Preferences from '../preferences/Preferences';
import DashBoard from '../dashboard/DashBoard';
import Login from '../login/Login';
import Home from '../home/Home';
import Roles from '../role/Roles';
import CreateRole from '../role/CreateRole';
import Header from './Header';

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
      <Header />
      <div className="wrapper">
        <Routes>
          <Route exact path="/" element={<Home />} />
          <Route exact path="/dashboard" element={<DashBoard />} />
          <Route exact path="/preferences" element={<Preferences />} />
          <Route exact path="/roles" element={<Roles />} />
          <Route exact path="/login" element={<Login />} />
          <Route exact path="/saverole" element={<CreateRole />} />
          <Route exact path="/saverole/:roleId" element={<CreateRole />} />
          <Route path="*" element={<NotPage />} />
        </Routes>
      </div>
    </>
  );
}

export default App;