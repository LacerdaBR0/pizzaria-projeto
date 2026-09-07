CREATE TABLE Categoria (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Nome        NVARCHAR(60) NOT NULL,
    Ativo       BIT NOT NULL DEFAULT 1
);

CREATE TABLE Produto (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    CategoriaId INT NOT NULL FOREIGN KEY REFERENCES Categoria(Id),
    Nome        NVARCHAR(120) NOT NULL,
    Descricao   NVARCHAR(400) NULL,
    Tipo        NVARCHAR(20) NOT NULL,
    Preco       DECIMAL(10,2) NOT NULL,
    Ativo       BIT NOT NULL DEFAULT 1
);

CREATE TABLE Tamanho (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Nome        NVARCHAR(30) NOT NULL,
    Fatias      INT NULL
);

CREATE TABLE PrecoPizza (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    ProdutoId   INT NOT NULL FOREIGN KEY REFERENCES Produto(Id),
    TamanhoId   INT NOT NULL FOREIGN KEY REFERENCES Tamanho(Id),
    Preco       DECIMAL(10,2) NOT NULL
);

CREATE TABLE Usuario (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Nome        NVARCHAR(120) NOT NULL,
    Email       NVARCHAR(160) NOT NULL UNIQUE,
    SenhaHash   NVARCHAR(256) NOT NULL,
    Perfil      NVARCHAR(20) NOT NULL DEFAULT 'Cliente',
    CriadoEm    DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);

CREATE TABLE Pedido (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId     INT NOT NULL FOREIGN KEY REFERENCES Usuario(Id),
    Endereco      NVARCHAR(300) NOT NULL,
    FormaPagamento NVARCHAR(30) NOT NULL,
    Status        NVARCHAR(30) NOT NULL DEFAULT 'Recebido',
    Total         DECIMAL(10,2) NOT NULL DEFAULT 0,
    CriadoEm      DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);

CREATE TABLE PedidoItem (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    PedidoId      INT NOT NULL FOREIGN KEY REFERENCES Pedido(Id),
    Descricao     NVARCHAR(300) NOT NULL,
    TamanhoId     INT NULL FOREIGN KEY REFERENCES Tamanho(Id),
    Sabor1Id      INT NULL FOREIGN KEY REFERENCES Produto(Id),
    Sabor2Id      INT NULL FOREIGN KEY REFERENCES Produto(Id),
    Quantidade    INT NOT NULL DEFAULT 1,
    PrecoUnitario DECIMAL(10,2) NOT NULL
);

INSERT INTO Categoria (Nome) VALUES ('Pizzas Salgadas'), ('Pizzas Doces'), ('Bebidas');
INSERT INTO Tamanho (Nome, Fatias) VALUES ('Média', 6), ('Grande', 8), ('Família', 12);
