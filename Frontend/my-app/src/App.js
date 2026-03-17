import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import NavBar from "./components/navbar"
import UserComponent from "./components/UserComponent";
import AddUserComponent from "./components/AddUserComponent";


function App() {
  return (
    <BrowserRouter>
      <div>
        {/* Top navigation bar */}
        <NavBar />

        <Routes>
          {/* Default route redirects to /list */}
          <Route path="/" element={<Navigate to="/list" />} />

          {/* List page */}
          <Route path="/list" element={<UserComponent />} />

          {/* Add page */}
          <Route path="/add" element={<AddUserComponent />} />
          <Route path="*" element={<p>Page not found</p>} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
