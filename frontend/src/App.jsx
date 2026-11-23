import { useEffect, useState } from "react";
import CardapioPage from "./components/CardapioPage.jsx";
import MontarPizzaPage from "./components/MontarPizzaPage.jsx";
import PedidosPage from "./components/PedidosPage.jsx";
import LoginPage from "./components/LoginPage.jsx";
import PainelPage from "./components/PainelPage.jsx";

export default function App() {
  const [aba, setAba] = useState("montar");
  const [usuario, setUsuario] = useState(null);

  useEffect(() => {
    try {
      const salvo = localStorage.getItem("usuario");
      if (salvo) setUsuario(JSON.parse(salvo));
    } catch (e) {
      setUsuario(null);
    }
  }, []);

  function entrar(u) {
    setUsuario(u);
    try {
      localStorage.setItem("usuario", JSON.stringify(u));
    } catch (e) {
      void e;
    }
    setAba("montar");
  }

  function sair() {
    setUsuario(null);
    try {
      localStorage.removeItem("usuario");
    } catch (e) {
      void e;
    }
    setAba("montar");
  }

  return (
    <div className="app">
      <header className="topo">
        <div className="marca">
          <span className="marca-icone">🍕</span>
          <div>
            <h1>Forno &amp; Lenha</h1>
            <p>Sistema da pizzaria</p>
          </div>
        </div>

        <nav className="abas">
          <button className={aba === "montar" ? "aba ativa" : "aba"} onClick={() => setAba("montar")}>
            Monte sua pizza
          </button>
          <button className={aba === "pedidos" ? "aba ativa" : "aba"} onClick={() => setAba("pedidos")}>
            Meus pedidos
          </button>
          <button className={aba === "cardapio" ? "aba ativa" : "aba"} onClick={() => setAba("cardapio")}>
            Cardápio
          </button>
          <button className={aba === "painel" ? "aba ativa" : "aba"} onClick={() => setAba("painel")}>
            Pizzaria
          </button>
        </nav>

        <div className="usuario-area">
          {usuario ? (
            <>
              <span className="usuario-nome">Olá, {usuario.nome}</span>
              <button className="btn-mini" onClick={sair}>Sair</button>
            </>
          ) : (
            <button className="btn-mini" onClick={() => setAba("login")}>Entrar</button>
          )}
        </div>
      </header>

      <main>
        {aba === "login" && <LoginPage onLogado={entrar} />}
        {aba === "montar" && <MontarPizzaPage usuario={usuario} onIrParaLogin={() => setAba("login")} />}
        {aba === "pedidos" && <PedidosPage usuario={usuario} onIrParaLogin={() => setAba("login")} />}
        {aba === "cardapio" && <CardapioPage />}
        {aba === "painel" && <PainelPage />}
      </main>
    </div>
  );
}
