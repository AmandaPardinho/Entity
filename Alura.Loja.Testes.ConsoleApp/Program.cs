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

                //foreach(var produto in produtos)
                //{
                //    Console.WriteLine(produto);
                //}

                //contexto herda de DbContext(classe base de toda a API do Entity) => possui o ChangeTracker => responsável por rastrear todas as mudanças que estão acontecendo numa determinada instância do contexto => possui uma lista(recuperada através do Entries) de todas as entidades que estão sendo gerenciadas num dado momento/contexto

                ExibeEntries(contexto);

                //Classe EntityEntry => retornada para cada um dos objetos disponíveis no banco de dados; possui uma propriedade essencial para o monitoramento de mudanças - a propriedade estado(registra o estado da entidade); caso haja uma alteração no estado, o método SaveChanges passa a ser requerido

                //var p1 = produtos.Last();
                //p1.Nome = "007: Cassino Royale";

                var novoProduto = new Produto()
                {
                    Nome = "Um Dia",
                    Categoria = "Filme",
                    Preco = 52.00
                };
                contexto.Produtos.Add(novoProduto);

                Console.WriteLine("=================");
                foreach (var e in contexto.ChangeTracker.Entries())
                {
                    Console.WriteLine(e.Entity.ToString() + " - " + e.State);
                }

                //ao acionar o SaveChanges, o Entity vai verificar qual estado teve alteração e acionar um comando SQL diferente de acordo com a alteração sofrida

                contexto.SaveChanges();

                //Console.WriteLine("=================");
                //foreach (var produto in produtos)
                //{
                //    Console.WriteLine(produto);
                //}
            }
        }

        private static void ExibeEntries(LojaContext contexto)
        {
            Console.WriteLine("=================");
            foreach (var e in contexto.ChangeTracker.Entries())
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }
    }
}
//ORM => mapeamento objeto relacional