# ▶️ AC3 — Pedido + login de cliente

Esta sprint adiciona cadastro e login de cliente, a finalização do carrinho em pedido
(com endereço e forma de pagamento) e o histórico de pedidos do cliente.

## 1. Atualizar os arquivos
Extraia o conteúdo da AC3 por cima da pasta do projeto, substituindo os arquivos quando
perguntado. Arquivos novos: Models Usuario/Pedido/PedidoItem, DTOs de auth e pedido,
AuthController, PedidosController, e os componentes React LoginPage e PedidosPage.
Arquivos alterados: AppDbContext, App, api.js, Carrinho, MontarPizzaPage, styles.css.

## 2. Criar as novas tabelas (migration)
Na pasta `backend`, com o `appsettings.json` apontando para o seu SQL Server (Server=NITRO):

```bash
cd backend
dotnet ef migrations add AC3_Pedido
dotnet ef database update
```

Isso cria as tabelas Usuario, Pedido e PedidoItem.

## 3. Rodar
```bash
cd backend
dotnet run
```
Em outro terminal:
```bash
cd frontend
npm run dev
```
Abra `http://localhost:5173`.

## Funcionalidade
- No topo, botão "Entrar": cadastro (nome, e-mail, senha) e login.
- A senha é guardada no banco com hash (SHA-256), não em texto puro.
- Monte uma pizza e adicione ao carrinho. Ao clicar em "Finalizar pedido", se não estiver
  logado o sistema leva para o login; logado, informa endereço e forma de pagamento.
- Ao confirmar, o carrinho vira um pedido salvo no banco e é esvaziado.
- A aba "Meus pedidos" lista o histórico do cliente logado.

## Testando
- Cadastre um cliente e faça login.
- Finalize um pedido e veja a confirmação.
- Abra "Meus pedidos" e confira o pedido no histórico.
- No SSMS, consulte as tabelas Usuario, Pedido e PedidoItem para mostrar a persistência.
