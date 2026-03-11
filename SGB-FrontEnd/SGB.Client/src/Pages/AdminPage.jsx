/* eslint-disable react-hooks/exhaustive-deps */

import { useState, useEffect, useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function AdminPage() {
  const { token } = useContext(AuthContext);

  const [employees, setEmployees] = useState([]);
  const [fee, setFee] = useState("");
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [password, setPassword] = useState("");

  useEffect(() => {
    if (token) {
      fetchEmployees();
      fetchConfig();
    }
  }, [token]);
  const fetchEmployees = async () => {
    const res = await fetch("http://localhost:5115/api/admin/empleados", {
      headers: { Authorization: `Bearer ${token}` },
    });
    const data = await res.json();
    setEmployees(data);
  };

  const fetchConfig = async () => {
    const res = await fetch("http://localhost:5115/api/admin/configuracion", {
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!res.ok) return;

    const text = await res.text();

    if (!text) return;

    const data = JSON.parse(text);
    setFee(data.feeDiario);
  };
  const createEmployee = async () => {
    await fetch("http://localhost:5115/api/admin/empleados", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        name,
        email,
        phone,
        password,
        rol: "Bibliotecario",
      }),
    });

    setName("");
    setEmail("");
    setPhone("");
    setPassword("");
    fetchEmployees();
  };

  const updateFee = async () => {
    await fetch("http://localhost:5115/api/admin/configuracion/fee", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(parseFloat(fee)),
    });
  };

  return (
    <div>
      <h2>Panel Admin</h2>

      <h3>Crear Bibliotecario</h3>
      <input
        placeholder="Nombre"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
      <input
        type="email"
        placeholder="Email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <input
        placeholder="Telefono"
        value={phone}
        onChange={(e) => setPhone(e.target.value)}
      />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={createEmployee}>Crear</button>

      <h3>Modificar Fee Diario</h3>
      <input value={fee} onChange={(e) => setFee(e.target.value)} />
      <button onClick={updateFee}>Actualizar Fee</button>

      <h3>Empleados</h3>
      <ul>
        {employees.map((e) => (
          <li key={e.id}>
            {e.name} - {e.role}
          </li>
        ))}
      </ul>
    </div>
  );
}
