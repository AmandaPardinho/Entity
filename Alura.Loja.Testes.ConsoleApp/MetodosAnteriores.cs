using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class MetodosAnteriores
    {
        private static void AtualizarProduto()
        {
            //incluir um produto
            GravarUsandoEntity();
            RecuperarProdutos();

            //atualizar o produto
            using (var contexto = new ProdutoDAOEntity())
            {
                Produto primeiroProduto = contexto.Produtos().First();
                primeiroProduto.Nome = "DVD 007: Cassino Royale";
                contexto.Atualizar(primeiroProduto);
            }
            RecuperarProdutos();
        }

        //deleta um produto do banco de dados
        private static void ExcluirProdutos()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = contexto.Produtos();
                foreach (var produto in produtos)
                {
                    contexto.Remover(produto);
                }
            }
        }

        //select usando o entity framework
        private static void RecuperarProdutos()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = contexto.Produtos();
                Console.WriteLine("Foram encontrados {0} produto(s).", produtos.Count);
                foreach (var produto in produtos)
                {
                    Console.WriteLine(produto.Nome);
                }

                Console.ReadKey();
            }
        }

        //cria um produto no banco de dados
        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnitario = 19.89;

            Produto p2 = new Produto();
            p2.Nome = "O Senhor dos Anéis - A Sociedade do Anel(Volume 1)";
            p2.Categoria = "Livros";
            p2.PrecoUnitario = 24.90;

            Produto p3 = new Produto();
            p3.Nome = "O Monge e o Executivo";
            p3.Categoria = "Livros";
            p3.PrecoUnitario = 30.99;

            Produto p4 = new Produto();
            p4.Nome = "007: Cassino Royale";
            p4.Categoria = "Filme";
            p4.PrecoUnitario = 49.90;

            using (var contexto = new ProdutoDAOEntity())
            {
                contexto.Adicionar(p4);
            }
        }

        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnitario = 19.89;

            //DAO => data access object
            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }

        //ChangeTracker
        //using(var contexto = new LojaContext())
        //{
        //    //gerando SQL através do Entity
        //    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
        //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        //    loggerFactory.AddProvider(SqlLoggerProvider.Create());

        //                var produtos = contexto.Produtos.ToList();
        //    ExibeEntries(contexto.ChangeTracker.Entries());

        //    var novoProduto = new Produto()
        //    {
        //        Nome = "Um Dia",
        //        Categoria = "Filme",
        //        PrecoUnitario = 52.00
        //    };
        //    contexto.Produtos.Add(novoProduto);
        //                ExibeEntries(contexto.ChangeTracker.Entries());

        //    contexto.Produtos.Remove(novoProduto);

        //                ExibeEntries(contexto.ChangeTracker.Entries());

        //    //contexto.SaveChanges();

        //    var entry = contexto.Entry(novoProduto);
        //    //ExibeEntries(contexto.ChangeTracker.Entries());
        //    Console.WriteLine(entry.Entity.ToString() + " - " + entry.State);
        //}

        public static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("=================");
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }

        private static void MuitosParaMuitos()
        {
            var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.79, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 12.45, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimentos", PrecoUnitario = 4.23, Unidade = "Gramas" };

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Páscoa Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);

            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                //contexto.Promocoes.Add(promocaoDePascoa);
                //MetodosAnteriores.ExibeEntries(contexto.ChangeTracker.Entries());

                var promocao = contexto.Promocoes.Find(3);
                contexto.Promocoes.Remove(promocao);
                contexto.SaveChanges();
            }
        }

        private static void UmParaUm()
        {
            var fulano = new Cliente();
            fulano.Nome = "Fulaninho de Tal";
            fulano.EnderecoEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua dos Inválidos",
                Complemento = "sobrado",
                Bairro = "Centro",
                Cidade = "Cidade"
            };

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }
        }
    }
}
