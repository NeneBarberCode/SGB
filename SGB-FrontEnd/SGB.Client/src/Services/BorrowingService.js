import { apiFetch } from "./api";

export async function getLoans() {
  const response = await apiFetch("/prestamos");

  if (!response.ok) {
    throw new Error("Error al obtener préstamos");
  }

  return response.json();
}
