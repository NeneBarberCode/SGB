import { useState } from "react";
import { AuthContext } from "../context/AuthContext";

function decodeToken(token) {
  try {
    const payload = JSON.parse(atob(token.split(".")[1]));
    return payload;
  } catch {
    return null;
  }
}

export function AuthProvider({ children }) {
  const [token, setToken] = useState(() => {
    return localStorage.getItem("token");
  });

  const [user, setUser] = useState(() => {
    const storedUser = localStorage.getItem("user");
    return storedUser ? JSON.parse(storedUser) : null;
  });

  const [role, setRole] = useState(() => {
    const storedToken = localStorage.getItem("token");
    if (!storedToken) return null;

    const decoded = decodeToken(storedToken);
    return (
      decoded?.[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ] || null
    );
  });

  const login = (data, tokenData) => {
    const decoded = decodeToken(tokenData);
    const userRole =
      decoded?.[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ] || null;

    localStorage.setItem("token", tokenData);
    localStorage.setItem("user", JSON.stringify(data));

    setUser(data);
    setToken(tokenData);
    setRole(userRole);
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");

    setUser(null);
    setToken(null);
    setRole(null);
  };

  const isSuperAdmin = role === "SuperAdmin";

  return (
    <AuthContext.Provider
      value={{ user, token, role, isSuperAdmin, login, logout }}
    >
      {children}
    </AuthContext.Provider>
  );
}
