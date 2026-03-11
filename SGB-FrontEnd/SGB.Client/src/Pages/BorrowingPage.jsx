/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect, useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function BorrowingPage() {
  const { token } = useContext(AuthContext);
  const [borrowings, setBorrowings] = useState([]);
  const [customerId, setCustomerId] = useState("");
  const [copyId, setCopyId] = useState("");
  const [customers, setCustomers] = useState([]);
  const [copies, setCopies] = useState([]);

  useEffect(() => {
    if (token) {
      fetchBorrowings();
      fetchCustomers();
      fetchCopies();
    }
  }, [token]);

  const fetchBorrowings = async () => {
    const res = await fetch("http://localhost:5115/api/borrowing", {
      headers: { Authorization: `Bearer ${token}` },
    });
    setBorrowings(await res.json());
  };

  const fetchCustomers = async () => {
    const res = await fetch("http://localhost:5115/api/customer", {
      headers: { Authorization: `Bearer ${token}` },
    });
    setCustomers(await res.json());
  };

  const fetchCopies = async () => {
    const res = await fetch("http://localhost:5115/api/copy", {
      headers: { Authorization: `Bearer ${token}` },
    });
    setCopies(await res.json());
  };

  const handleCreate = async () => {
    const res = await fetch("http://localhost:5115/api/borrowing", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        customerId: parseInt(customerId),
        copyId: parseInt(copyId),
      }),
    });
    if (res.ok) fetchBorrowings();
    else alert("No se pudo crear préstamo.");
  };

  const handleReturn = async (id) => {
    const res = await fetch(
      `http://localhost:5115/api/borrowing/${id}/devolver`,
      {
        method: "POST",
        headers: { Authorization: `Bearer ${token}` },
      },
    );
    if (res.ok) fetchBorrowings();
  };

  return (
    <div>
      <h2>Préstamos</h2>

      <div>
        <select
          value={customerId}
          onChange={(e) => setCustomerId(e.target.value)}
        >
          <option value="">Selecciona un cliente</option>
          {customers.map((c) => (
            <option key={c.id} value={c.id}>
              {c.name}
            </option>
          ))}
        </select>

        <select value={copyId} onChange={(e) => setCopyId(e.target.value)}>
          <option value="">Selecciona un ejemplar</option>
          {copies.map((e) => (
            <option key={e.id} value={e.id}>
              {e.bookTitle}
            </option>
          ))}
        </select>

        <button onClick={handleCreate}>Crear préstamo</button>
      </div>

      <ul>
        {borrowings.map((b) => (
          <li key={b.id}>
            {b.customer} - {b.book} - {b.accumulatedFee} $
            {b.fechaDevolucion === null && (
              <button onClick={() => handleReturn(b.id)}>Devolver</button>
            )}
          </li>
        ))}
      </ul>
      <div>
        <h2>Lista de Préstamos</h2>

        <table border="1" cellPadding="8">
          <thead>
            <tr>
              <th>ID</th>
              <th>Libro</th>
              <th>Usuario</th>
              <th>Fecha Préstamo</th>
              <th>Fecha Devolución</th>
              <th>Estado</th>
              <th>Acciones</th>
            </tr>
          </thead>

          <tbody>
            {borrowings.map((b) => (
              <tr key={b.id}>
                <td>{b.id}</td>
                <td>{b.book}</td>
                <td>{b.customer}</td>
                <td>{new Date(b.borrowDate).toLocaleDateString()}</td>
                <td>
                  {b.returnDate
                    ? new Date(b.returnDate).toLocaleDateString()
                    : "—"}
                </td>
                <td>{b.returnDate ? "Devuelto" : "Activo"}</td>
                <td>
                  {!b.returnDate && (
                    <button onClick={() => handleReturn(b.id)}>Devolver</button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
