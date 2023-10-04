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
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = new Promocao();
                promocao.Descricao = "Queima total janeiro 2024";
                promocao.DataInicio = new DateTime(2024, 1, 1);
                promocao.DataTermino = new DateTime(2024, 1, 31);

                var produtos = contexto
                    .Produtos
                    .Where(p => p.Categoria == "Bebidas")
                    .ToList();

                foreach(var produto in produtos)
                {
                    promocao.IncluiProduto(produto);
                }

                contexto.Promocoes.Add(promocao);

                MetodosAnteriores.ExibeEntries(contexto.ChangeTracker.Entries());

                contexto.SaveChanges();
            }

            //o Entity e outras ORMs não recuperam as entidades relacionadas junto com SELECT que foi feito(comportamento padrão) => melhora a performance, já que impede que muitos objetos sejam recuperados de uma única vez
            using(var contexto2 = new LojaContext())
            {
                var promocao = contexto2.Promocoes.FirstOrDefault();

                Console.WriteLine("\nMostrando os produtos da promoção\n");

                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
            }
        }        
    }
}