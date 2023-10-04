using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
    //DbContext: permite que a classe use a API do Entity dentro dela
    public class LojaContext : DbContext
    {
        //DbSet: informa qual classe será persistida
        //Propriedade (produtos): possui o mesmo nome da tabela do banco de dados; representa o conjunto de objetos da classe 
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public LojaContext()
        {
            
        }

        //injeta dependência
        //recebe as opções de configuração de conexão com o banco de dados 
        public LojaContext(DbContextOptions<LojaContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PromocaoProduto>()
                .HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });

            modelBuilder
                .Entity<Endereco>()
                .ToTable("Enderecos");

            modelBuilder
                .Entity<Endereco>()
                .Property<int>("ClienteId"); //shadow property

            modelBuilder
                .Entity<Endereco>()
                .HasKey("ClienteId");
        }

        //define qual o banco de dados usado e o seu endereço
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //usar o "if" como optionBuilder da linha 34 dentro se for usar método da linha 20 - necessário pois tanto o construtor quanto o OnConfiguring foram usados
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-MIHQT5G;Database=Loja;user=sa;password=amanda03;Trusted_Connection=true;TrustServerCertificate=True");
            }
        }
    }
}