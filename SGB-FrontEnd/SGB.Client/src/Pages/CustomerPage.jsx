import { useState, useEffect, useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function CustomerPage() {
  const { token } = useContext(AuthContext);

  const [customers, setCustomers] = useState([]);
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    if (token) {
      fetchCustomers();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [token]);

  const fetchCustomers = async () => {
    const res = await fetch("http://localhost:5115/api/customer", {
      headers: { Authorization: `Bearer ${token}` },
    });

    const data = await res.json();
    setCustomers(data);
  };

  const handleCreate = async () => {
    setError("");

    if (!name || !email || !phone) {
      setError("Todos los campos son obligatorios");
      return;
    }

    const res = await fetch("http://localhost:5115/api/customer", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        name,
        email,
        phone,
      }),
    });

    if (res.ok) {
      setName("");
      setEmail("");
      setPhone("");
      fetchCustomers();
    } else {
      setError("No se pudo crear el cliente");
    }
  };

  return (
    <div>
      <h2>Clientes</h2>

      <div style={{ marginBottom: "20px" }}>
        <h3>Registrar Cliente</h3>

        <input
          type="text"
          placeholder="Nombre"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <br />
        <br />

        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <br />
        <br />

        <input
          type="text"
          placeholder="Teléfono"
          value={phone}
          onChange={(e) => setPhone(e.target.value)}
        />
        <br />
        <br />

        <button onClick={handleCreate}>Registrar</button>

        {error && <p style={{ color: "red" }}>{error}</p>}
      </div>

      <h3>Lista de Clientes</h3>

      <table border="1" cellPadding="8">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Email</th>
            <th>Teléfono</th>
            <th>Fecha Registro</th>
          </tr>
        </thead>

        <tbody>
          {customers.map((c) => (
            <tr key={c.id}>
              <td>{c.id}</td>
              <td>{c.name}</td>
              <td>{c.email}</td>
              <td>{c.phone}</td>
              <td>{c.registrationDate?.split("T")[0] || "—"}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
