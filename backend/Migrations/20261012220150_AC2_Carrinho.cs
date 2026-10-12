using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pizzaria.Api.Migrations
{
    /// <inheritdoc />
    public partial class AC2_Carrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrinho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tamanho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Fatias = table.Column<int>(type: "int", nullable: true),
                    Fator = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tamanho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrinhoId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TamanhoId = table.Column<int>(type: "int", nullable: false),
                    Sabor1Id = table.Column<int>(type: "int", nullable: false),
                    Sabor2Id = table.Column<int>(type: "int", nullable: true),
                    BordaId = table.Column<int>(type: "int", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoItem_Carrinho_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoItem_Produto_BordaId",
                        column: x => x.BordaId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarrinhoItem_Produto_Sabor1Id",
                        column: x => x.Sabor1Id,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarrinhoItem_Produto_Sabor2Id",
                        column: x => x.Sabor2Id,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarrinhoItem_Tamanho_TamanhoId",
                        column: x => x.TamanhoId,
                        principalTable: "Tamanho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoItemAdicional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrinhoItemId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItemAdicional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoItemAdicional_CarrinhoItem_CarrinhoItemId",
                        column: x => x.CarrinhoItemId,
                        principalTable: "CarrinhoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoItemAdicional_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Tamanho",
                columns: new[] { "Id", "Fatias", "Fator", "Nome" },
                values: new object[,]
                {
                    { 1, 6, 1.00m, "Média" },
                    { 2, 8, 1.30m, "Grande" },
                    { 3, 12, 1.60m, "Família" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItem_BordaId",
                table: "CarrinhoItem",
                column: "BordaId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItem_CarrinhoId",
                table: "CarrinhoItem",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItem_Sabor1Id",
                table: "CarrinhoItem",
                column: "Sabor1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItem_Sabor2Id",
                table: "CarrinhoItem",
                column: "Sabor2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItem_TamanhoId",
                table: "CarrinhoItem",
                column: "TamanhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItemAdicional_CarrinhoItemId",
                table: "CarrinhoItemAdicional",
                column: "CarrinhoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItemAdicional_ProdutoId",
                table: "CarrinhoItemAdicional",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoItemAdicional");

            migrationBuilder.DropTable(
                name: "CarrinhoItem");

            migrationBuilder.DropTable(
                name: "Carrinho");

            migrationBuilder.DropTable(
                name: "Tamanho");
        }
    }
}
