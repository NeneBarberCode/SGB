import { useContext } from "react";
import { AuthContext } from "../context/AuthContext.jsx";
import { Link, Routes, Route } from "react-router-dom";
import BookPage from "./BooksPage.jsx";
import CopyPage from "./CopiesPage.jsx";
import BorrowingPage from "./BorrowingPage.jsx";
import ProtectedRoute from "../components/ProtectedRoute.jsx";
import AdminPage from "./AdminPage.jsx";
import CustomerPage from "./CustomerPage.jsx";
export default function Dashboard() {
  const { user, logout, isSuperAdmin } = useContext(AuthContext);

  return (
    <div>
      <header>
        <h1>Dashboard</h1>
        <p>
          Bienvenido, {user.Name} ({user.role})
        </p>
        <button onClick={logout}>Cerrar sesión</button>
      </header>

      <nav>
        <Link to="book">Libros</Link> | <Link to="copy">Ejemplares</Link> |{" "}
        <Link to="borrowing">Prestamos</Link> |{" "}
        <Link to="customer">Clientes</Link> |{" "}
        {isSuperAdmin && <Link to="admin">Admin</Link>}
      </nav>

      <main>
        <Routes>
          <Route path="book" element={<BookPage />} />
          <Route path="copy" element={<CopyPage />} />
          <Route path="borrowing" element={<BorrowingPage />} />
          <Route path="customer" element={<CustomerPage />} />
          <Route
            path="admin"
            element={
              <ProtectedRoute>
                <AdminPage />
              </ProtectedRoute>
            }
          />
          <Route path="*" element={<p>Selecciona una opción del menú.</p>} />
        </Routes>
      </main>
    </div>
  );
}
