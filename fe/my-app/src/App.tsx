import React from 'react';
import logo from './logo.svg';
import './App.scss';
import {Navigation} from "./layout/navigation/Navigation";
import {BrowserRouter} from "react-router-dom";
import ProfilePage from "./pages/profile/ProfilePage";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
          <Navigation />
          <ProfilePage />
      </BrowserRouter>
    </div>
  );
}

export default App;
