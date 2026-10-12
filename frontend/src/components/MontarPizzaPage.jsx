import { useEffect, useState } from "react";
import { api } from "../api.js";
import MontarPizza from "./MontarPizza.jsx";
import Carrinho from "./Carrinho.jsx";

export default function MontarPizzaPage() {
  const [tamanhos, setTamanhos] = useState([]);
  const [produtos, setProdutos] = useState([]);
  const [carrinho, setCarrinho] = useState(null);
  const [carregando, setCarregando] = useState(true);
  const [erro, setErro] = useState("");

  async function carregar() {
    try {
      setCarregando(true);
      const [listaTamanhos, listaProdutos, carr] = await Promise.all([
        api.listarTamanhos(),
        api.listarProdutos(),
        api.obterCarrinho()
      ]);
      setTamanhos(listaTamanhos);
      setProdutos(listaProdutos);
      setCarrinho(carr);
      setErro("");
    } catch (e) {
      setErro("Não foi possível carregar os dados. Verifique se a API está no ar.");
    } finally {
      setCarregando(false);
    }
  }

  useEffect(() => {
    carregar();
  }, []);

  const sabores = produtos.filter((p) => p.tipo === "Pizza" && p.ativo);
  const bordas = produtos.filter((p) => p.tipo === "Borda" && p.ativo);
  const adicionais = produtos.filter((p) => p.tipo === "Adicional" && p.ativo);

  async function adicionar(dto) {
    try {
      const atualizado = await api.adicionarItemCarrinho(dto);
      setCarrinho(atualizado);
    } catch (e) {
      setErro(e.message);
    }
  }

  async function alterarQuantidade(id, quantidade) {
    try {
      const atualizado = await api.atualizarQuantidade(id, quantidade);
      setCarrinho(atualizado);
    } catch (e) {
      setErro(e.message);
    }
  }

  async function remover(id) {
    try {
      const atualizado = await api.removerItemCarrinho(id);
      setCarrinho(atualizado);
    } catch (e) {
      setErro(e.message);
    }
  }

  async function esvaziar() {
    try {
      const atualizado = await api.esvaziarCarrinho();
      setCarrinho(atualizado);
    } catch (e) {
      setErro(e.message);
    }
  }

  if (carregando) {
    return (
      <div className="vazio">
        <p>Carregando…</p>
      </div>
    );
  }

  return (
    <div>
      {erro && <div className="alerta">{erro}</div>}
      {sabores.length === 0 && (
        <div className="alerta">
          Nenhum sabor cadastrado. Cadastre pizzas (tipo "Pizza") na aba Cardápio para montar.
        </div>
      )}
      <div className="conteudo">
        <section className="coluna-form">
          <MontarPizza
            tamanhos={tamanhos}
            sabores={sabores}
            bordas={bordas}
            adicionais={adicionais}
            onAdicionar={adicionar}
          />
        </section>
        <aside className="coluna-carrinho">
          <Carrinho
            carrinho={carrinho}
            onAlterarQuantidade={alterarQuantidade}
            onRemover={remover}
            onEsvaziar={esvaziar}
          />
        </aside>
      </div>
    </div>
  );
}
