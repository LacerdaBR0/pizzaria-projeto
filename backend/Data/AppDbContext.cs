using Microsoft.EntityFrameworkCore;
using Pizzaria.Api.Models;

namespace Pizzaria.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Tamanho> Tamanhos => Set<Tamanho>();
    public DbSet<Carrinho> Carrinhos => Set<Carrinho>();
    public DbSet<CarrinhoItem> CarrinhoItens => Set<CarrinhoItem>();
    public DbSet<CarrinhoItemAdicional> CarrinhoItemAdicionais => Set<CarrinhoItemAdicional>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(e =>
        {
            e.ToTable("Categoria");
            e.Property(c => c.Nome).HasMaxLength(60).IsRequired();
        });

        modelBuilder.Entity<Produto>(e =>
        {
            e.ToTable("Produto");
            e.Property(p => p.Nome).HasMaxLength(120).IsRequired();
            e.Property(p => p.Descricao).HasMaxLength(400);
            e.Property(p => p.Tipo).HasMaxLength(20).IsRequired();
            e.Property(p => p.Preco).HasColumnType("decimal(10,2)");
            e.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId);
        });

        modelBuilder.Entity<Tamanho>(e =>
        {
            e.ToTable("Tamanho");
            e.Property(t => t.Nome).HasMaxLength(30).IsRequired();
            e.Property(t => t.Fator).HasColumnType("decimal(6,2)");
        });

        modelBuilder.Entity<Carrinho>(e =>
        {
            e.ToTable("Carrinho");
        });

        modelBuilder.Entity<CarrinhoItem>(e =>
        {
            e.ToTable("CarrinhoItem");
            e.Property(i => i.Descricao).HasMaxLength(300).IsRequired();
            e.Property(i => i.PrecoUnitario).HasColumnType("decimal(10,2)");

            e.HasOne(i => i.Carrinho)
                .WithMany(c => c.Itens)
                .HasForeignKey(i => i.CarrinhoId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(i => i.Tamanho)
                .WithMany()
                .HasForeignKey(i => i.TamanhoId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(i => i.Sabor1)
                .WithMany()
                .HasForeignKey(i => i.Sabor1Id)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(i => i.Sabor2)
                .WithMany()
                .HasForeignKey(i => i.Sabor2Id)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(i => i.Borda)
                .WithMany()
                .HasForeignKey(i => i.BordaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CarrinhoItemAdicional>(e =>
        {
            e.ToTable("CarrinhoItemAdicional");

            e.HasOne(a => a.CarrinhoItem)
                .WithMany(i => i.Adicionais)
                .HasForeignKey(a => a.CarrinhoItemId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(a => a.Produto)
                .WithMany()
                .HasForeignKey(a => a.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Pizzas Salgadas", Ativo = true },
            new Categoria { Id = 2, Nome = "Pizzas Doces", Ativo = true },
            new Categoria { Id = 3, Nome = "Bebidas", Ativo = true }
        );

        modelBuilder.Entity<Tamanho>().HasData(
            new Tamanho { Id = 1, Nome = "Média", Fatias = 6, Fator = 1.00m },
            new Tamanho { Id = 2, Nome = "Grande", Fatias = 8, Fator = 1.30m },
            new Tamanho { Id = 3, Nome = "Família", Fatias = 12, Fator = 1.60m }
        );
    }
}
