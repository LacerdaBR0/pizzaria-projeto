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

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Pizzas Salgadas", Ativo = true },
            new Categoria { Id = 2, Nome = "Pizzas Doces", Ativo = true },
            new Categoria { Id = 3, Nome = "Bebidas", Ativo = true }
        );
    }
}
