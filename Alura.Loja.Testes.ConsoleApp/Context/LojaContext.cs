using Alura.Loja.Testes.ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class LojaContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public LojaContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromocaoProduto>()
                .HasKey(pp => new { pp.PromocoesId, pp.ProdutosId });
            modelBuilder.Entity<Endereco>().Property<int>("ClienteId");

            modelBuilder.Entity<Endereco>().HasKey("ClienteId");

            modelBuilder.Entity<Endereco>().ToTable("Enderecos");


            base.OnModelCreating(modelBuilder);
        }

        public LojaContext(DbContextOptions<LojaContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
            }
        }

        
    }
}