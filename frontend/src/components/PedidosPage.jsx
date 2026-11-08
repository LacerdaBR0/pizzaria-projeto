import { useEffect, useState } from "react";
import { api } from "../api.js";

function moeda(valor) {
  return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

function dataHora(iso) {
  return new Date(iso).toLocaleString("pt-BR");
}

export default function PedidosPage({ usuario, onIrParaLogin }) {
  const [pedidos, setPedidos] = useState([]);
  const [carregando, setCarregando] = useState(true);
  const [erro, setErro] = useState("");

  useEffect(() => {
    if (!usuario) {
      setCarregando(false);
      return;
    }
    async function carregar() {
      try {
        setCarregando(true);
        const lista = await api.listarPedidos(usuario.id);
        setPedidos(lista);
        setErro("");
      } catch (e) {
        setErro("Não foi possível carregar os pedidos.");
      } finally {
        setCarregando(false);
      }
    }
    carregar();
  }, [usuario]);

  if (!usuario) {
    return (
      <div className="vazio">
        <p>Entre para ver seus pedidos.</p>
        <button className="btn btn-primario" onClick={onIrParaLogin}>
          Entrar
        </button>
      </div>
    );
  }

  if (carregando) {
    return (
      <div className="vazio">
        <p>Carregando pedidos…</p>
      </div>
    );
  }

  if (erro) {
    return <div className="alerta">{erro}</div>;
  }

  if (pedidos.length === 0) {
    return (
      <div className="vazio">
        <p>Você ainda não fez pedidos.</p>
        <span>Monte uma pizza e finalize o pedido para vê-lo aqui.</span>
      </div>
    );
  }

  return (
    <div className="pedidos">
      {pedidos.map((p) => (
        <div key={p.id} className="painel pedido-card">
          <div className="pedido-cabecalho">
            <div>
              <strong>Pedido #{p.id}</strong>
              <span className="pedido-data">{dataHora(p.criadoEm)}</span>
            </div>
            <span className="tag tag-ativo">{p.status}</span>
          </div>

          <ul className="pedido-itens">
            {p.itens.map((i, idx) => (
              <li key={idx}>
                <span>{i.quantidade}x {i.descricao}</span>
                <span className="preco">{moeda(i.subtotal)}</span>
              </li>
            ))}
          </ul>

          <div className="pedido-rodape">
            <span>{p.formaPagamento} · {p.endereco}</span>
            <strong>{moeda(p.total)}</strong>
          </div>
        </div>
      ))}
    </div>
  );
}
