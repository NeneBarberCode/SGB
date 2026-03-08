import { useContext } from "react";
import { AuthContext } from "../context/AuthContext.jsx";
import { Link, Routes, Route } from "react-router-dom";
import LibrosPage from "./BooksPage.jsx";
import EjemplaresPage from "./CopiesPage.jsx";
import ClientesPage from "./CustomerPage.jsx";
import PrestamosPage from "./BorrowingPage.jsx";
import ProtectedRoute from "../components/ProtectedRoute.jsx";
import AdminPage from "./AdminPage.jsx";
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
        <Link to="libros">Libros</Link> |{" "}
        <Link to="ejemplares">Ejemplares</Link> |{" "}
        <Link to="prestamos">Prestamos</Link> |{" "}
        <Link to="clientes">Clientes</Link> |{" "}
        {isSuperAdmin && <Link to="admin">Admin</Link>}
      </nav>

      <main>
        <Routes>
          <Route path="libros" element={<LibrosPage />} />
          <Route path="ejemplares" element={<EjemplaresPage />} />
          <Route path="prestamos" element={<PrestamosPage />} />
          <Route path="clientes" element={<ClientesPage />} />
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
