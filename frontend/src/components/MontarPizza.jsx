import { useMemo, useState } from "react";

function moeda(valor) {
  return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

export default function MontarPizza({ tamanhos, sabores, bordas, adicionais, onAdicionar }) {
  const [tamanhoId, setTamanhoId] = useState("");
  const [meioAMeio, setMeioAMeio] = useState(false);
  const [sabor1Id, setSabor1Id] = useState("");
  const [sabor2Id, setSabor2Id] = useState("");
  const [bordaId, setBordaId] = useState("");
  const [adicionaisSel, setAdicionaisSel] = useState([]);
  const [quantidade, setQuantidade] = useState(1);
  const [erro, setErro] = useState("");

  function alternarAdicional(id) {
    setAdicionaisSel((atual) =>
      atual.includes(id) ? atual.filter((x) => x !== id) : [...atual, id]
    );
  }

  const precoUnitario = useMemo(() => {
    const tamanho = tamanhos.find((t) => t.id === Number(tamanhoId));
    const s1 = sabores.find((s) => s.id === Number(sabor1Id));
    const s2 = meioAMeio ? sabores.find((s) => s.id === Number(sabor2Id)) : null;
    if (!tamanho || !s1) return null;
    if (meioAMeio && !s2) return null;

    const precoSabor = s2 ? Math.max(s1.preco, s2.preco) : s1.preco;
    let preco = precoSabor * tamanho.fator;
    const borda = bordas.find((b) => b.id === Number(bordaId));
    if (borda) preco += borda.preco;
    preco += adicionais
      .filter((a) => adicionaisSel.includes(a.id))
      .reduce((soma, a) => soma + a.preco, 0);
    return Math.round(preco * 100) / 100;
  }, [tamanhoId, sabor1Id, sabor2Id, meioAMeio, bordaId, adicionaisSel, tamanhos, sabores, bordas, adicionais]);

  const totalItem = useMemo(() => {
    if (precoUnitario === null) return null;
    const qtd = Number(quantidade) || 0;
    return Math.round(precoUnitario * qtd * 100) / 100;
  }, [precoUnitario, quantidade]);

  function enviar() {
    setErro("");
    if (!tamanhoId || !sabor1Id) {
      setErro("Escolha o tamanho e ao menos um sabor.");
      return;
    }
    if (meioAMeio && !sabor2Id) {
      setErro("Escolha o segundo sabor da meio-a-meio.");
      return;
    }
    onAdicionar({
      tamanhoId: Number(tamanhoId),
      sabor1Id: Number(sabor1Id),
      sabor2Id: meioAMeio ? Number(sabor2Id) : null,
      bordaId: bordaId ? Number(bordaId) : null,
      adicionaisIds: adicionaisSel,
      quantidade: Number(quantidade)
    });
  }

  return (
    <div className="painel">
      <h2>Monte sua pizza</h2>

      {erro && <div className="alerta">{erro}</div>}

      <label className="campo">
        <span>Tamanho</span>
        <select value={tamanhoId} onChange={(e) => setTamanhoId(e.target.value)}>
          <option value="">Selecione</option>
          {tamanhos.map((t) => (
            <option key={t.id} value={t.id}>
              {t.nome}{t.fatias ? ` (${t.fatias} fatias)` : ""}
            </option>
          ))}
        </select>
      </label>

      <label className="campo campo-inline">
        <input
          type="checkbox"
          checked={meioAMeio}
          onChange={(e) => {
            setMeioAMeio(e.target.checked);
            if (!e.target.checked) setSabor2Id("");
          }}
        />
        <span>Pizza meio-a-meio (2 sabores)</span>
      </label>

      <label className="campo">
        <span>{meioAMeio ? "Sabor 1" : "Sabor"}</span>
        <select value={sabor1Id} onChange={(e) => setSabor1Id(e.target.value)}>
          <option value="">Selecione</option>
          {sabores.map((s) => (
            <option key={s.id} value={s.id}>
              {s.nome} — {moeda(s.preco)}
            </option>
          ))}
        </select>
      </label>

      {meioAMeio && (
        <label className="campo">
          <span>Sabor 2</span>
          <select value={sabor2Id} onChange={(e) => setSabor2Id(e.target.value)}>
            <option value="">Selecione</option>
            {sabores.map((s) => (
              <option key={s.id} value={s.id}>
                {s.nome} — {moeda(s.preco)}
              </option>
            ))}
          </select>
          <small className="dica">Na meio-a-meio, vale o preço do sabor mais caro.</small>
        </label>
      )}

      <label className="campo">
        <span>Borda</span>
        <select value={bordaId} onChange={(e) => setBordaId(e.target.value)}>
          <option value="">Sem borda</option>
          {bordas.map((b) => (
            <option key={b.id} value={b.id}>
              {b.nome} — {moeda(b.preco)}
            </option>
          ))}
        </select>
      </label>

      {adicionais.length > 0 && (
        <div className="campo">
          <span>Adicionais</span>
          <div className="adicionais">
            {adicionais.map((a) => (
              <label key={a.id} className="adicional">
                <input
                  type="checkbox"
                  checked={adicionaisSel.includes(a.id)}
                  onChange={() => alternarAdicional(a.id)}
                />
                <span>{a.nome} — {moeda(a.preco)}</span>
              </label>
            ))}
          </div>
        </div>
      )}

      <label className="campo">
        <span>Quantidade</span>
        <input
          type="number"
          min="1"
          value={quantidade}
          onChange={(e) => setQuantidade(e.target.value)}
        />
      </label>

      <div className="preco-preview preco-total-item">
        <span>Total</span>
        <strong>{totalItem === null ? "—" : moeda(totalItem)}</strong>
      </div>

      <div className="acoes">
        <button className="btn btn-primario" onClick={enviar}>
          Adicionar ao carrinho
        </button>
      </div>
    </div>
  );
}
