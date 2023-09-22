using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var contexto = new LojaContext())
            {
                //gerando SQL através do Entity
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var produtos = contexto.Produtos.ToList();
                ExibeEntries(contexto.ChangeTracker.Entries());

                var novoProduto = new Produto()
                {
                    Nome = "Um Dia",
                    Categoria = "Filme",
                    Preco = 52.00
                };
                contexto.Produtos.Add(novoProduto);
                ExibeEntries(contexto.ChangeTracker.Entries());

                contexto.Produtos.Remove(novoProduto);

                ExibeEntries(contexto.ChangeTracker.Entries());

                //contexto.SaveChanges();

                var entry = contexto.Entry(novoProduto);
                //ExibeEntries(contexto.ChangeTracker.Entries());
                Console.WriteLine(entry.Entity.ToString() + " - " + entry.State);
            }
        }

        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("=================");
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }
    }
}
//ORM => mapeamento objeto relacional