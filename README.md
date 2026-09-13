# 🍕 Sistema de Pizzaria

Projeto de software desenvolvido em sprints (4 entregas incrementais), com arquitetura
em 3 camadas: **front-end**, **back-end** e **banco de dados**.

## Arquitetura

| Camada        | Tecnologia            |
|---------------|-----------------------|
| Front-end     | React (Vite)          |
| Back-end      | ASP.NET Core Web API  |
| Banco de dados| SQL Server            |

## Estrutura do repositório

```
/frontend    → aplicação React
/backend     → API .NET
/database    → scripts SQL / migrations
/docs        → board, roteiros dos vídeos e links das entregas
```

## Entregas

| Entrega   | Data      | Funcionalidade                          | Vídeo | Board |
|-----------|-----------|-----------------------------------------|-------|-------|
| AC1       | 14/09     | Cardápio (CRUD)                         | _link_ | _link_ |
| AC2       | 13/10     | Monte sua pizza + carrinho              | _link_ | _link_ |
| AC3       | 08/11     | Pedido + login                          | _link_ | _link_ |
| Final     | 22/11     | Painel da pizzaria + relatório          | _link_ | _link_ |

> Substitua os `_link_` pelo link do vídeo de cada sprint e do board.

## Como rodar (a preencher ao longo do desenvolvimento)

### Banco
1. Execute os scripts em `/database` na ordem numérica.

### Back-end
```bash
cd backend
dotnet run
```

### Front-end
```bash
cd frontend
npm install
npm run dev
```

## Autor
Guilherme Silva de Lacerda.
