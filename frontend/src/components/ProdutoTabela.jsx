function moeda(valor) {
  return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

export default function ProdutoTabela({ produtos, onEditar, onRemover }) {
  if (produtos.length === 0) {
    return (
      <div className="vazio">
        <p>O cardápio está vazio.</p>
        <span>Adicione o primeiro item pelo formulário ao lado.</span>
      </div>
    );
  }

  return (
    <table className="tabela">
      <thead>
        <tr>
          <th>Item</th>
          <th>Categoria</th>
          <th>Tipo</th>
          <th>Preço</th>
          <th>Status</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {produtos.map((p) => (
          <tr key={p.id}>
            <td>
              <strong>{p.nome}</strong>
              {p.descricao && <div className="descricao">{p.descricao}</div>}
            </td>
            <td>{p.categoriaNome}</td>
            <td>{p.tipo}</td>
            <td className="preco">{moeda(p.preco)}</td>
            <td>
              <span className={p.ativo ? "tag tag-ativo" : "tag tag-inativo"}>
                {p.ativo ? "Ativo" : "Inativo"}
              </span>
            </td>
            <td className="col-acoes">
              <button className="btn-mini" onClick={() => onEditar(p)}>
                Editar
              </button>
              <button className="btn-mini btn-mini-perigo" onClick={() => onRemover(p)}>
                Excluir
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
