import React from "react";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Login from "./pages/Login";
import Branches from "./pages/admin/Branches";
import BranchForm from "./pages/admin/BranchForm";
import Booking from "./pages/Booking";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Branches />} />
        <Route path="/login" element={<Login />} />
        <Route path="/branch" element={<Branches />} />
        <Route path="/branch/create" element={<BranchForm />} />
        <Route path="/booking" element={<Booking />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
