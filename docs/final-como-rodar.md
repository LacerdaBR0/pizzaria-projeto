# ▶️ Entrega Final — Painel da pizzaria + relatório de vendas

Esta entrega adiciona o painel administrativo da pizzaria: acompanhar os pedidos e avançar
o status (Recebido → Em preparo → Saiu pra entrega → Entregue), um dashboard de vendas
(faturamento, nº de pedidos, ticket médio, mais vendidas, pedidos por status) e exportação
do relatório em CSV. Não há mudança no banco — a coluna Status já existe desde a AC3, então
NÃO precisa de migration.

## 1. Substituir / adicionar os arquivos
Back-end:
- backend/Controllers/PedidosController.cs (substituir)
- backend/Controllers/RelatorioController.cs (novo)
- backend/Dtos/PedidoDtos.cs (substituir)
- backend/Dtos/RelatorioDtos.cs (novo)

Front-end:
- frontend/src/api.js (substituir)
- frontend/src/App.jsx (substituir)
- frontend/src/components/PainelPage.jsx (novo)
- frontend/src/styles.css (substituir)

## 2. Conferir o build (sem migration)
```bash
cd backend
dotnet build
```
Se der "Construir êxito", está pronto. Não rode migration — não há mudança de tabela.

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
Abra http://localhost:5173 e clique na aba "Pizzaria".

## Testando
- Faça alguns pedidos como cliente (aba Monte sua pizza > Finalizar pedido).
- Abra a aba "Pizzaria": veja o faturamento, o número de pedidos e o ticket médio.
- Em cada pedido, mude o status pelo seletor (Recebido → Em preparo → Saiu pra entrega → Entregue).
- Veja o ranking de mais vendidas e a contagem por status.
- Clique em "Exportar CSV" para baixar o relatório.
