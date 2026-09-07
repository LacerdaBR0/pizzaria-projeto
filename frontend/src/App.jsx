import { useEffect, useState } from "react";
import { api } from "./api.js";
import ProdutoForm from "./components/ProdutoForm.jsx";
import ProdutoTabela from "./components/ProdutoTabela.jsx";

export default function App() {
  const [produtos, setProdutos] = useState([]);
  const [categorias, setCategorias] = useState([]);
  const [emEdicao, setEmEdicao] = useState(null);
  const [carregando, setCarregando] = useState(true);
  const [erro, setErro] = useState("");

  async function carregar() {
    try {
      setCarregando(true);
      const [listaProdutos, listaCategorias] = await Promise.all([
        api.listarProdutos(),
        api.listarCategorias()
      ]);
      setProdutos(listaProdutos);
      setCategorias(listaCategorias);
      setErro("");
    } catch (e) {
      setErro("Não foi possível carregar o cardápio. Verifique se a API está no ar.");
    } finally {
      setCarregando(false);
    }
  }

  useEffect(() => {
    carregar();
  }, []);

  async function salvar(dados) {
    try {
      if (emEdicao) {
        await api.atualizarProduto(emEdicao.id, dados);
      } else {
        await api.criarProduto(dados);
      }
      setEmEdicao(null);
      await carregar();
    } catch (e) {
      setErro(e.message);
    }
  }

  async function remover(produto) {
    const confirmar = window.confirm(`Excluir "${produto.nome}" do cardápio?`);
    if (!confirmar) return;
    try {
      await api.removerProduto(produto.id);
      if (emEdicao && emEdicao.id === produto.id) setEmEdicao(null);
      await carregar();
    } catch (e) {
      setErro(e.message);
    }
  }

  const totalAtivos = produtos.filter((p) => p.ativo).length;

  return (
    <div className="app">
      <header className="topo">
        <div className="marca">
          <span className="marca-icone">🍕</span>
          <div>
            <h1>Forno &amp; Lenha</h1>
            <p>Gestão do cardápio</p>
          </div>
        </div>
        <div className="resumo">
          <div>
            <strong>{produtos.length}</strong>
            <span>itens</span>
          </div>
          <div>
            <strong>{totalAtivos}</strong>
            <span>ativos</span>
          </div>
        </div>
      </header>

      {erro && <div className="alerta">{erro}</div>}

      <main className="conteudo">
        <section className="coluna-lista">
          {carregando ? (
            <div className="vazio">
              <p>Carregando cardápio…</p>
            </div>
          ) : (
            <ProdutoTabela produtos={produtos} onEditar={setEmEdicao} onRemover={remover} />
          )}
        </section>

        <aside className="coluna-form">
          <ProdutoForm
            categorias={categorias}
            emEdicao={emEdicao}
            onSalvar={salvar}
            onCancelar={() => setEmEdicao(null)}
          />
        </aside>
      </main>
    </div>
  );
}
