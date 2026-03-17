
import { Link } from "react-router-dom";
import "./user.css";

export default function NavBar() {
  return (
    <nav>
      <Link to="/list">List</Link> | <Link to="/add">Add</Link>
    </nav>
  );
}

