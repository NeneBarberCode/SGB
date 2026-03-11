import { useState, useEffect, useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function BookPage() {
  const { token } = useContext(AuthContext);
  const [book, setBook] = useState([]);
  const [title, setTitle] = useState("");
  const [author, setAuthor] = useState("");
  const [isbn, setIsbn] = useState("");

  // get books
  useEffect(() => {
    const token = localStorage.getItem("token");

    fetch("http://localhost:5115/api/book", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((res) => {
        if (!res.ok) throw new Error("No autorizado");
        return res.json();
      })
      .then(setBook)
      .catch(console.error);
  }, []);

  // create book
  const handleCreate = async () => {
    const res = await fetch("http://localhost:5115/api/book", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({ title, author, isbn }),
    });
    if (res.ok) {
      const newBook = await res.json();
      setBook([...book, newBook]);
      setTitle("");
      setAuthor("");
      setIsbn("");
    }
  };

  return (
    <div>
      <h2>Libros</h2>

      <div>
        <input
          placeholder="Título"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <input
          placeholder="Autor"
          value={author}
          onChange={(e) => setAuthor(e.target.value)}
        />
        <input
          placeholder="ISBN"
          value={isbn}
          onChange={(e) => setIsbn(e.target.value)}
        />
        <button onClick={handleCreate}>Agregar</button>
      </div>

      <ul>
        {book.map((b) => (
          <li key={b.id}>
            {b.title} - {b.author} - {b.isbn}
          </li>
        ))}
      </ul>
    </div>
  );
}
