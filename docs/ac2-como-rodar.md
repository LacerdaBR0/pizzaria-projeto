# ▶️ AC2 — Monte sua pizza + carrinho

Esta sprint adiciona a montagem de pizza (com meio-a-meio, borda e adicionais) e o
carrinho persistido no banco, sobre o CRUD do cardápio da AC1.

## 1. Atualizar os arquivos
Extraia o conteúdo da AC2 por cima da pasta do projeto, substituindo os arquivos quando
perguntado. Arquivos novos e alterados: Models de carrinho e tamanho, AppDbContext,
CarrinhoController, TamanhosController, e os componentes React da montagem e do carrinho.

## 2. Criar as novas tabelas (migration)
Na pasta `backend`, com o `appsettings.json` apontando para o seu SQL Server (Server=NITRO):

```bash
cd backend
dotnet ef migrations add AC2_Carrinho
dotnet ef database update
```

Isso cria as tabelas Tamanho, Carrinho, CarrinhoItem e CarrinhoItemAdicional, e já insere
os três tamanhos (Média, Grande, Família) com seus fatores de preço.

## 3. Popular dados de exemplo (opcional, recomendado para o vídeo)
No SSMS, abra e execute o script `database/02_seed_ac2.sql`. Ele cadastra alguns sabores,
bordas e adicionais para você já ter o que montar.

> Se já tiver cadastrado itens na AC1, revise para não duplicar.

## 4. Rodar
```bash
cd backend
dotnet run
```
Em outro terminal:
```bash
cd frontend
npm run dev
```
Abra `http://localhost:5173` e use a aba "Monte sua pizza".

## Regra de preço (calculada no servidor)
- Meio-a-meio cobra o preço do sabor mais caro dos dois.
- O tamanho multiplica o preço base (Média 1.0, Grande 1.3, Família 1.6).
- Borda e adicionais somam ao total.

## Testando a funcionalidade
- Monte uma pizza meio-a-meio com dois sabores de preços diferentes e confira que vale o mais caro.
- Troque o tamanho e veja o preço mudar.
- Adicione borda e adicionais.
- Adicione ao carrinho, altere a quantidade, remova um item e confira o total.
- No SSMS, consulte CarrinhoItem para mostrar a persistência no banco.
