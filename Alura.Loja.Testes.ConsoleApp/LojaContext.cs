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

        public LojaContext()
        {
            
        }

        //injeta dependência
        //recebe as opções de configuração de conexão com o banco de dados 
        public LojaContext(DbContextOptions<LojaContext> options): base(options) { }

        //define qual o banco de dados usado e o seu endereço
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                //construtor de opções
                optionsBuilder.UseSqlServer("Server=DESKTOP-MIHQT5G;Database=Loja;user=sa;password=amanda03;Trusted_Connection=true;TrustServerCertificate=True");
            }
        }
    }
}