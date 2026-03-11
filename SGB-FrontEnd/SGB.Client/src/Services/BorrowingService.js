import { apiFetch } from "./api";

export async function getLoans() {
  const response = await apiFetch("/borrowing");

  if (!response.ok) {
    throw new Error("Error al obtener préstamos");
  }

  return response.json();
}
