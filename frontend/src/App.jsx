import { useState } from "react";
import CardapioPage from "./components/CardapioPage.jsx";
import MontarPizzaPage from "./components/MontarPizzaPage.jsx";

export default function App() {
  const [aba, setAba] = useState("montar");

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
          <button
            className={aba === "montar" ? "aba ativa" : "aba"}
            onClick={() => setAba("montar")}
          >
            Monte sua pizza
          </button>
          <button
            className={aba === "cardapio" ? "aba ativa" : "aba"}
            onClick={() => setAba("cardapio")}
          >
            Cardápio
          </button>
        </nav>
      </header>

      <main>
        {aba === "montar" ? <MontarPizzaPage /> : <CardapioPage />}
      </main>
    </div>
  );
}
