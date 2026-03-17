import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import NavBar from "./components/navbar"
import UserComponent from "./components/userComponent";
import AddUser from "./components/addUser";


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
          <Route path="/add" element={<AddUser />} />
          <Route path="*" element={<p>Page not found</p>} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
