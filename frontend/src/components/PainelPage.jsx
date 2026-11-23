import { useEffect, useState } from "react";
import { api } from "../api.js";

const STATUS = ["Recebido", "Em preparo", "Saiu pra entrega", "Entregue"];

function moeda(valor) {
  return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

function dataHora(iso) {
  return new Date(iso).toLocaleString("pt-BR");
}

export default function PainelPage() {
  const [pedidos, setPedidos] = useState([]);
  const [relatorio, setRelatorio] = useState(null);
  const [carregando, setCarregando] = useState(true);
  const [erro, setErro] = useState("");

  async function carregar() {
    try {
      setCarregando(true);
      const [lista, rel] = await Promise.all([
        api.listarTodosPedidos(),
        api.obterRelatorio()
      ]);
      setPedidos(lista);
      setRelatorio(rel);
      setErro("");
    } catch (e) {
      setErro("Não foi possível carregar o painel. Verifique se a API está no ar.");
    } finally {
      setCarregando(false);
    }
  }

  useEffect(() => {
    carregar();
  }, []);

  async function mudarStatus(id, status) {
    try {
      await api.atualizarStatusPedido(id, status);
      await carregar();
    } catch (e) {
      setErro(e.message);
    }
  }

  function exportarCsv() {
    const linhas = [["Pedido", "Data", "Cliente", "Status", "Pagamento", "Total"]];
    pedidos.forEach((p) => {
      linhas.push([
        p.id,
        dataHora(p.criadoEm),
        p.cliente,
        p.status,
        p.formaPagamento,
        p.total.toFixed(2).replace(".", ",")
      ]);
    });
    const csv = linhas.map((l) => l.map((c) => `"${c}"`).join(";")).join("\n");
    const blob = new Blob(["\ufeff" + csv], { type: "text/csv;charset=utf-8;" });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = "relatorio-vendas.csv";
    a.click();
    URL.revokeObjectURL(url);
  }

  if (carregando) {
    return (
      <div className="vazio">
        <p>Carregando painel…</p>
      </div>
    );
  }

  if (erro) {
    return <div className="alerta">{erro}</div>;
  }

  return (
    <div className="painel-admin">
      <section className="cartoes">
        <div className="cartao">
          <span>Faturamento</span>
          <strong>{moeda(relatorio.totalFaturado)}</strong>
        </div>
        <div className="cartao">
          <span>Pedidos</span>
          <strong>{relatorio.totalPedidos}</strong>
        </div>
        <div className="cartao">
          <span>Ticket médio</span>
          <strong>{moeda(relatorio.ticketMedio)}</strong>
        </div>
      </section>

      <div className="conteudo">
        <section className="coluna-form">
          <div className="painel">
            <div className="carrinho-topo">
              <h2>Pedidos</h2>
              <button className="btn-mini" onClick={exportarCsv}>Exportar CSV</button>
            </div>

            {pedidos.length === 0 ? (
              <div className="vazio-carrinho">
                <p>Nenhum pedido ainda.</p>
                <span>Os pedidos dos clientes aparecem aqui.</span>
              </div>
            ) : (
              <div className="pedidos">
                {pedidos.map((p) => (
                  <div key={p.id} className="pedido-card">
                    <div className="pedido-cabecalho">
                      <div>
                        <strong>Pedido #{p.id}</strong>
                        <span className="pedido-data">{dataHora(p.criadoEm)}</span>
                      </div>
                      <select
                        className="status-select"
                        value={p.status}
                        onChange={(e) => mudarStatus(p.id, e.target.value)}
                      >
                        {STATUS.map((s) => (
                          <option key={s} value={s}>{s}</option>
                        ))}
                      </select>
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
                      <span>{p.cliente} · {p.formaPagamento} · {p.endereco}</span>
                      <strong>{moeda(p.total)}</strong>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>
        </section>

        <aside className="coluna-carrinho">
          <div className="painel">
            <h2>Mais vendidas</h2>
            {relatorio.maisVendidas.length === 0 ? (
              <p className="dica">Sem dados ainda.</p>
            ) : (
              <ul className="ranking">
                {relatorio.maisVendidas.map((m, idx) => (
                  <li key={idx}>
                    <span className="ranking-pos">{idx + 1}</span>
                    <span className="ranking-nome">{m.descricao}</span>
                    <span className="ranking-qtd">{m.quantidade}</span>
                  </li>
                ))}
              </ul>
            )}

            <h2 style={{ marginTop: "20px" }}>Por status</h2>
            <ul className="ranking">
              {relatorio.porStatus.map((s, idx) => (
                <li key={idx}>
                  <span className="ranking-nome">{s.status}</span>
                  <span className="ranking-qtd">{s.quantidade}</span>
                </li>
              ))}
            </ul>
          </div>
        </aside>
      </div>
    </div>
  );
}
