using ApiSportArena.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace ApiSportArena.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Carrinho> Carrinho { get; set; }

        public DbSet<CarrinhoItem> CarrinhoItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);

            //carrinhoItem
            modelBuilder.Entity<CarrinhoItem>()
                .HasOne(p => p.Carrinho)
                .WithMany(c => c.Itens)
                .HasForeignKey(p => p.CarrinhoId)
                .OnDelete(DeleteBehavior.Restrict);

            // CarrinhoItem -> Produto
            modelBuilder.Entity<CarrinhoItem>()
                .HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId);
        }
    }
}
