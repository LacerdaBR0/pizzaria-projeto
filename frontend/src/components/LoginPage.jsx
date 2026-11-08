import { useState } from "react";
import { api } from "../api.js";

export default function LoginPage({ onLogado }) {
  const [modo, setModo] = useState("login");
  const [nome, setNome] = useState("");
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");
  const [erro, setErro] = useState("");
  const [enviando, setEnviando] = useState(false);

  async function enviar() {
    setErro("");
    if (!email || !senha || (modo === "registro" && !nome)) {
      setErro("Preencha todos os campos.");
      return;
    }
    try {
      setEnviando(true);
      let usuario;
      if (modo === "registro") {
        usuario = await api.registrar({ nome, email, senha });
      } else {
        usuario = await api.login({ email, senha });
      }
      onLogado(usuario);
    } catch (e) {
      setErro(e.message);
    } finally {
      setEnviando(false);
    }
  }

  return (
    <div className="auth-wrap">
      <div className="painel auth-painel">
        <h2>{modo === "login" ? "Entrar" : "Criar conta"}</h2>

        {erro && <div className="alerta">{erro}</div>}

        {modo === "registro" && (
          <label className="campo">
            <span>Nome</span>
            <input value={nome} onChange={(e) => setNome(e.target.value)} placeholder="Seu nome" />
          </label>
        )}

        <label className="campo">
          <span>E-mail</span>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="voce@email.com"
          />
        </label>

        <label className="campo">
          <span>Senha</span>
          <input
            type="password"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
            placeholder="••••••"
          />
        </label>

        <div className="acoes">
          <button className="btn btn-primario" onClick={enviar} disabled={enviando}>
            {enviando ? "Aguarde…" : modo === "login" ? "Entrar" : "Criar conta"}
          </button>
        </div>

        <p className="auth-troca">
          {modo === "login" ? "Ainda não tem conta?" : "Já tem conta?"}{" "}
          <button
            className="link-botao"
            onClick={() => {
              setErro("");
              setModo(modo === "login" ? "registro" : "login");
            }}
          >
            {modo === "login" ? "Criar conta" : "Entrar"}
          </button>
        </p>
      </div>
    </div>
  );
}
