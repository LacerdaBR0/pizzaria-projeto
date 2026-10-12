function moeda(valor) {
  return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

export default function Carrinho({ carrinho, onAlterarQuantidade, onRemover, onEsvaziar }) {
  const itens = carrinho?.itens ?? [];

  return (
    <div className="painel">
      <div className="carrinho-topo">
        <h2>Carrinho</h2>
        {itens.length > 0 && (
          <button className="btn-mini btn-mini-perigo" onClick={onEsvaziar}>
            Esvaziar
          </button>
        )}
      </div>

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
        </>
      )}
    </div>
  );
}
