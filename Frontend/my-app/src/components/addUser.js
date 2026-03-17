import { useState } from "react";
import { addUser } from "../service";
import { useNavigate } from "react-router-dom";

export default function AddUser() {
  const [form, setForm] = useState({ name:"", age:"", city:"", state:"", pin:"" });
  const [errors, setErrors] = useState({});
  const navigate = useNavigate();

  function validate() {
    const errs = {};
    if (form.name.length < 2 || form.name.length > 100) errs.name = "Name must be 2-100 chars";
    if (form.age < 0 || form.age > 120) errs.age = "Age must be 0-120";
    if (!form.city) errs.city = "City required";
    if (!form.state) errs.state = "State required";
    if (form.pin.length < 4 || form.pin.length > 10) errs.pin = "Pin must be 4-10 chars";
    return errs;
  }

  async function handleSubmit(e) {
    e.preventDefault();
    const errs = validate();
    if (Object.keys(errs).length) { setErrors(errs); return; }
    try {
      await addUser(form);
      alert("User added successfully!");
      navigate("/list");
    } catch (err) {
      alert("Error: " + err.message);
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      {["name","age","city","state","pin"].map(field => (
        <div key={field}>
          <label>{field}</label>
          <input
            type={field==="age" ? "number" : "text"}
            value={form[field]}
            onChange={e => setForm({...form, [field]: e.target.value})}
          />
          {errors[field] && <span style={{color:"red"}}>{errors[field]}</span>}
        </div>
      ))}
      <button type="submit">Add User</button>
    </form>
  );
}