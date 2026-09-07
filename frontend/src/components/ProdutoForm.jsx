import { useEffect, useState } from "react";

const vazio = {
  categoriaId: "",
  nome: "",
  descricao: "",
  tipo: "Pizza",
  preco: "",
  ativo: true
};

export default function ProdutoForm({ categorias, emEdicao, onSalvar, onCancelar }) {
  const [form, setForm] = useState(vazio);

  useEffect(() => {
    if (emEdicao) {
      setForm({
        categoriaId: String(emEdicao.categoriaId),
        nome: emEdicao.nome,
        descricao: emEdicao.descricao ?? "",
        tipo: emEdicao.tipo,
        preco: String(emEdicao.preco),
        ativo: emEdicao.ativo
      });
    } else {
      setForm(vazio);
    }
  }, [emEdicao]);

  function atualizar(campo, valor) {
    setForm((atual) => ({ ...atual, [campo]: valor }));
  }

  function enviar() {
    if (!form.categoriaId || !form.nome || form.preco === "") return;
    onSalvar({
      categoriaId: Number(form.categoriaId),
      nome: form.nome.trim(),
      descricao: form.descricao.trim() || null,
      tipo: form.tipo,
      preco: Number(form.preco),
      ativo: form.ativo
    });
  }

  return (
    <div className="painel">
      <h2>{emEdicao ? "Editar item" : "Novo item"}</h2>

      <label className="campo">
        <span>Nome</span>
        <input
          value={form.nome}
          onChange={(e) => atualizar("nome", e.target.value)}
          placeholder="Ex: Calabresa"
        />
      </label>

      <label className="campo">
        <span>Categoria</span>
        <select
          value={form.categoriaId}
          onChange={(e) => atualizar("categoriaId", e.target.value)}
        >
          <option value="">Selecione</option>
          {categorias.map((c) => (
            <option key={c.id} value={c.id}>
              {c.nome}
            </option>
          ))}
        </select>
      </label>

      <label className="campo">
        <span>Tipo</span>
        <select value={form.tipo} onChange={(e) => atualizar("tipo", e.target.value)}>
          <option value="Pizza">Pizza</option>
          <option value="Bebida">Bebida</option>
          <option value="Borda">Borda</option>
          <option value="Adicional">Adicional</option>
        </select>
      </label>

      <label className="campo">
        <span>Preço</span>
        <input
          type="number"
          step="0.01"
          min="0"
          value={form.preco}
          onChange={(e) => atualizar("preco", e.target.value)}
          placeholder="0,00"
        />
      </label>

      <label className="campo">
        <span>Descrição</span>
        <textarea
          rows="2"
          value={form.descricao}
          onChange={(e) => atualizar("descricao", e.target.value)}
          placeholder="Molho, mussarela, calabresa e cebola"
        />
      </label>

      <label className="campo campo-inline">
        <input
          type="checkbox"
          checked={form.ativo}
          onChange={(e) => atualizar("ativo", e.target.checked)}
        />
        <span>Item ativo no cardápio</span>
      </label>

      <div className="acoes">
        <button className="btn btn-primario" onClick={enviar}>
          {emEdicao ? "Salvar alterações" : "Adicionar ao cardápio"}
        </button>
        {emEdicao && (
          <button className="btn btn-neutro" onClick={onCancelar}>
            Cancelar
          </button>
        )}
      </div>
    </div>
  );
}
