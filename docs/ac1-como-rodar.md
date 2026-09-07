# ▶️ AC1 — Como rodar (CRUD do cardápio)

Pré-requisitos: .NET 8 SDK, Node.js 18+, SQL Server e a ferramenta `dotnet-ef`.

## 1. Banco de dados
O app usa EF Core e cria as tabelas por migration. Ajuste a connection string em
`backend/appsettings.json` se o seu SQL Server não for `localhost` com autenticação do Windows.

```bash
cd backend
dotnet tool install --global dotnet-ef
dotnet ef migrations add Inicial
dotnet ef database update
```

Isso cria o banco `Pizzaria` com as tabelas `Categoria` e `Produto` e as categorias iniciais.

> O arquivo `database/01_schema.sql` contém o schema completo (incluindo tabelas das
> próximas ACs). Ele serve como entregável/documentação do banco; para rodar a AC1 use
> as migrations acima.

## 2. Back-end
```bash
cd backend
dotnet run
```
A API sobe em `http://localhost:5000` e o Swagger em `http://localhost:5000/swagger`.

## 3. Front-end
```bash
cd frontend
npm install
npm run dev
```
Abra `http://localhost:5173`.

## Testando a funcionalidade
- Criar um item pelo formulário à direita.
- Ver o item na tabela.
- Editar o preço.
- Excluir com confirmação.
- Conferir o registro na tabela `Produto` do SQL Server.
