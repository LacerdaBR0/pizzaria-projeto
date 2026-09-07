const BASE_URL = "http://localhost:5000/api";

async function handle(res) {
  if (!res.ok) {
    const texto = await res.text();
    throw new Error(texto || "Erro na requisição.");
  }
  if (res.status === 204) return null;
  return res.json();
}

export const api = {
  listarProdutos: () => fetch(`${BASE_URL}/produtos`).then(handle),
  criarProduto: (dados) =>
    fetch(`${BASE_URL}/produtos`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(dados)
    }).then(handle),
  atualizarProduto: (id, dados) =>
    fetch(`${BASE_URL}/produtos/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(dados)
    }).then(handle),
  removerProduto: (id) =>
    fetch(`${BASE_URL}/produtos/${id}`, { method: "DELETE" }).then(handle),
  listarCategorias: () => fetch(`${BASE_URL}/categorias`).then(handle)
};
