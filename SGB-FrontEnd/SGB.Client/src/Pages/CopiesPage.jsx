import { useState, useEffect, useContext, useCallback } from "react";
import { AuthContext } from "../context/AuthContext";

export default function CopyPage() {
  const { token } = useContext(AuthContext);
  const [copies, setCopies] = useState([]);
  const [books, setBooks] = useState([]);
  const [bookId, setBookId] = useState("");

  // get copies
  const fetchCopies = useCallback(async () => {
    const res = await fetch("http://localhost:5115/api/copy", {
      headers: { Authorization: `Bearer ${token}` },
    });
    const data = await res.json();
    setCopies(data);
  }, [token]);
  // get books for selection
  const fetchBooks = useCallback(async () => {
    const res = await fetch("http://localhost:5115/api/book", {
      headers: { Authorization: `Bearer ${token}` },
    });
    const data = await res.json();
    setBooks(data);
  }, [token]);

  useEffect(() => {
    const loadData = async () => {
      fetchCopies();
      fetchBooks();
    };
    loadData();
  }, [fetchCopies, fetchBooks]);

  // create copy
  const handleCreate = async () => {
    if (!bookId) return alert("Selecciona un libro");
    const res = await fetch("http://localhost:5115/api/copy", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({ bookId: parseInt(bookId) }),
    });
    if (res.ok) {
      setBookId("");
      fetchCopies();
    }
  };

  // delete copy
  const handleDelete = async (id) => {
    const res = await fetch(`http://localhost:5115/api/copy/${id}`, {
      method: "DELETE",
      headers: { Authorization: `Bearer ${token}` },
    });
    if (res.ok) fetchCopies();
  };

  return (
    <div>
      <h2>Ejemplares</h2>

      <div>
        <select value={bookId} onChange={(e) => setBookId(e.target.value)}>
          <option value="">Selecciona un libro</option>
          {books.map((b) => (
            <option key={b.id} value={b.id}>
              {b.title}
            </option>
          ))}
        </select>
        <button onClick={handleCreate}>Agregar ejemplar</button>
      </div>

      <ul>
        {copies.map((e) => (
          <li key={e.id}>
            {e.bookTitle} - {e.available ? "Disponible" : "Prestado"}
            <button onClick={() => handleDelete(e.id)}>Eliminar</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
