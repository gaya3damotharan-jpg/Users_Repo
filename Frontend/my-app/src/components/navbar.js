import { Link } from "react-router-dom";

export default function NavBar() {
  return (
    <nav>
      <Link to="/list">List</Link> | <Link to="/add">Add</Link>
    </nav>
  );
}