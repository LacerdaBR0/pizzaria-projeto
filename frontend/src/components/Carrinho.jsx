import { useState } from "react";

function moeda(valor) {
  return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

export default function Carrinho({
  carrinho,
  usuario,
  onAlterarQuantidade,
  onRemover,
  onEsvaziar,
  onFinalizar,
  onIrParaLogin
}) {
  const itens = carrinho?.itens ?? [];
  const [finalizando, setFinalizando] = useState(false);
  const [endereco, setEndereco] = useState("");
  const [pagamento, setPagamento] = useState("Dinheiro");
  const [erro, setErro] = useState("");
  const [sucesso, setSucesso] = useState("");

  async function confirmar() {
    setErro("");
    if (!endereco.trim()) {
      setErro("Informe o endereço de entrega.");
      return;
    }
    try {
      const pedido = await onFinalizar({ endereco: endereco.trim(), formaPagamento: pagamento });
      setSucesso(`Pedido #${pedido.id} realizado com sucesso!`);
      setFinalizando(false);
      setEndereco("");
      setPagamento("Dinheiro");
    } catch (e) {
      setErro(e.message);
    }
  }

  return (
    <div className="painel">
      <div className="carrinho-topo">
        <h2>Carrinho</h2>
        {itens.length > 0 && !finalizando && (
          <button className="btn-mini btn-mini-perigo" onClick={onEsvaziar}>
            Esvaziar
          </button>
        )}
      </div>

      {sucesso && <div className="sucesso">{sucesso}</div>}

      {itens.length === 0 ? (
        <div className="vazio-carrinho">
          <p>Seu carrinho está vazio.</p>
          <span>Monte uma pizza ao lado para começar.</span>
        </div>
      ) : (
        <>
          <ul className="carrinho-lista">
            {itens.map((i) => (
              <li key={i.id} className="carrinho-item">
                <div className="carrinho-item-info">
                  <strong>{i.descricao}</strong>
                  <span className="carrinho-preco">{moeda(i.precoUnitario)} cada</span>
                </div>
                <div className="carrinho-item-controles">
                  <div className="qtd">
                    <button onClick={() => onAlterarQuantidade(i.id, i.quantidade - 1)} disabled={i.quantidade <= 1}>
                      −
                    </button>
                    <span>{i.quantidade}</span>
                    <button onClick={() => onAlterarQuantidade(i.id, i.quantidade + 1)}>+</button>
                  </div>
                  <strong className="carrinho-subtotal">{moeda(i.subtotal)}</strong>
                  <button className="btn-remover" onClick={() => onRemover(i.id)}>
                    Remover
                  </button>
                </div>
              </li>
            ))}
          </ul>

          <div className="carrinho-total">
            <span>Total</span>
            <strong>{moeda(carrinho.total)}</strong>
          </div>

          {!finalizando ? (
            <div className="acoes">
              <button
                className="btn btn-primario"
                onClick={() => {
                  setSucesso("");
                  if (!usuario) {
                    onIrParaLogin();
                  } else {
                    setFinalizando(true);
                  }
                }}
              >
                Finalizar pedido
              </button>
            </div>
          ) : (
            <div className="finalizar">
              <h3>Finalizar pedido</h3>
              {erro && <div className="alerta">{erro}</div>}
              <label className="campo">
                <span>Endereço de entrega</span>
                <input
                  value={endereco}
                  onChange={(e) => setEndereco(e.target.value)}
                  placeholder="Rua, número, bairro"
                />
              </label>
              <label className="campo">
                <span>Forma de pagamento</span>
                <select value={pagamento} onChange={(e) => setPagamento(e.target.value)}>
                  <option value="Dinheiro">Dinheiro</option>
                  <option value="Cartão de crédito">Cartão de crédito</option>
                  <option value="Cartão de débito">Cartão de débito</option>
                  <option value="Pix">Pix</option>
                </select>
              </label>
              <div className="acoes">
                <button className="btn btn-primario" onClick={confirmar}>
                  Confirmar pedido
                </button>
                <button className="btn btn-neutro" onClick={() => setFinalizando(false)}>
                  Voltar
                </button>
              </div>
            </div>
          )}
        </>
      )}
    </div>
  );
}
