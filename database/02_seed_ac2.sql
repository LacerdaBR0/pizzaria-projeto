USE Pizzaria;

INSERT INTO Produto (CategoriaId, Nome, Descricao, Tipo, Preco, Ativo) VALUES
(1, N'Calabresa', N'Molho, mussarela, calabresa e cebola', N'Pizza', 49.90, 1),
(1, N'Mussarela', N'Molho, mussarela e orégano', N'Pizza', 45.90, 1),
(1, N'Portuguesa', N'Presunto, ovo, cebola, ervilha e mussarela', N'Pizza', 54.90, 1),
(1, N'Frango com Catupiry', N'Frango desfiado e catupiry', N'Pizza', 56.90, 1),
(1, N'Quatro Queijos', N'Mussarela, provolone, parmesao e catupiry', N'Pizza', 58.90, 1),
(1, N'Marguerita', N'Molho, mussarela, tomate e manjericao', N'Pizza', 52.90, 1),
(1, N'Pepperoni', N'Molho, mussarela e pepperoni', N'Pizza', 59.90, 1),
(1, N'Bacon', N'Molho, mussarela e bacon', N'Pizza', 55.90, 1),
(1, N'Vegetariana', N'Mussarela, tomate, pimentao, cebola, champignon e azeitona', N'Pizza', 53.90, 1),
(1, N'Toscana', N'Mussarela, calabresa moida, bacon e cebola', N'Pizza', 57.90, 1);

INSERT INTO Produto (CategoriaId, Nome, Descricao, Tipo, Preco, Ativo) VALUES
(2, N'Chocolate', N'Chocolate ao leite', N'Pizza', 44.90, 1),
(2, N'Chocolate com Morango', N'Chocolate ao leite e morango', N'Pizza', 49.90, 1),
(2, N'Banana com Canela', N'Banana, acucar e canela', N'Pizza', 42.90, 1),
(2, N'Romeu e Julieta', N'Mussarela e goiabada', N'Pizza', 46.90, 1),
(2, N'Prestigio', N'Chocolate e coco', N'Pizza', 47.90, 1);

INSERT INTO Produto (CategoriaId, Nome, Descricao, Tipo, Preco, Ativo) VALUES
(3, N'Refrigerante Lata 350ml', N'Lata 350ml', N'Bebida', 6.00, 1),
(3, N'Refrigerante 2L', N'Garrafa 2 litros', N'Bebida', 12.00, 1),
(3, N'Suco Natural 500ml', N'Laranja ou maracuja', N'Bebida', 9.00, 1),
(3, N'Agua Mineral 500ml', N'Com ou sem gas', N'Bebida', 4.00, 1),
(3, N'Cerveja Lata 350ml', N'Lata 350ml', N'Bebida', 8.00, 1);

INSERT INTO Produto (CategoriaId, Nome, Descricao, Tipo, Preco, Ativo) VALUES
(1, N'Borda Catupiry', NULL, N'Borda', 8.00, 1),
(1, N'Borda Cheddar', NULL, N'Borda', 8.00, 1),
(1, N'Borda Chocolate', NULL, N'Borda', 9.00, 1),
(1, N'Borda Cream Cheese', NULL, N'Borda', 9.00, 1);

INSERT INTO Produto (CategoriaId, Nome, Descricao, Tipo, Preco, Ativo) VALUES
(1, N'Bacon extra', NULL, N'Adicional', 6.00, 1),
(1, N'Mussarela extra', NULL, N'Adicional', 5.00, 1),
(1, N'Catupiry extra', NULL, N'Adicional', 5.00, 1),
(1, N'Cheddar extra', NULL, N'Adicional', 5.00, 1),
(1, N'Azeitona', NULL, N'Adicional', 3.00, 1),
(1, N'Champignon', NULL, N'Adicional', 4.00, 1),
(1, N'Cebola caramelizada', NULL, N'Adicional', 4.00, 1);
