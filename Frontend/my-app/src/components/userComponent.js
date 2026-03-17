import { useEffect, useState } from "react";
import { fetchUsers } from "../service";

export default function UserComponent() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchUsers()
      .then(setUsers)
      .catch(err => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error}</p>;
  if (users.length === 0) return <p>No users found.</p>;

  return (
    <table>
      <thead>
        <tr><th>Name</th><th>Age</th><th>City</th><th>State</th><th>Pin</th></tr>
      </thead>
      <tbody>
        {users.map(u => (
          <tr key={u.id}>
            <td>{u.name}</td><td>{u.age}</td><td>{u.city}</td><td>{u.state}</td><td>{u.pin}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}