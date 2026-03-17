const API_URL = "http://localhost:5000/api/users";

export async function fetchUsers() {
  console.log(API_URL);
  const res = await fetch(API_URL);
  if (!res.ok) throw new Error("Failed to load users");
  return res.json();
}

export async function addUser(user) {
  const res = await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user),
  });
  if (!res.ok) throw new Error("Failed to add user");
  return res.json();
}