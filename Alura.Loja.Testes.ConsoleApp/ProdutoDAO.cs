using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class ProdutoDAO : IDisposable, IProdutoDAO
    {
        private SqlConnection conexao;

        public ProdutoDAO()
        {
            //uso do Ado.Net e do ProviderSqlServer
            this.conexao = new SqlConnection("Server=DESKTOP-MIHQT5G;Database=Loja;user=sa;password=amanda03;Trusted_Connection=true;TrustServerCertificate=True");
            this.conexao.Open();
        }

        public void Dispose()
        {
            this.conexao.Close();
        }

        //CRUD USANDO ADO.NET
        //insert usando Ado.Net
        public void Adicionar(Produto p)
        {
            try
            {
                var insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Produtos (Nome, Categoria, PrecoUnitario) VALUES (@nome, @categoria, @precoUnitario)";

                var paramNome = new SqlParameter("nome", p.Nome);
                insertCmd.Parameters.Add(paramNome);

                var paramCategoria = new SqlParameter("categoria", p.Categoria);
                insertCmd.Parameters.Add(paramCategoria);

                var paramPrecoUnitario = new SqlParameter("preco", p.PrecoUnitario);
                insertCmd.Parameters.Add(paramPrecoUnitario);

                insertCmd.ExecuteNonQuery();
            } catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        //update usando Ado.Net
        public void Atualizar(Produto p)
        {
            try
            {
                var updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "UPDATE Produtos SET Nome = @nome, Categoria = @categoria, PrecoUnitario = @precoUnitario WHERE Id = @id";

                var paramNome = new SqlParameter("nome", p.Nome);
                var paramCategoria = new SqlParameter("categoria", p.Categoria);
                var paramPrecoUnitario = new SqlParameter("precoUnitario", p.PrecoUnitario);
                var paramId = new SqlParameter("id", p.Id);
                updateCmd.Parameters.Add(paramNome);
                updateCmd.Parameters.Add(paramCategoria);
                updateCmd.Parameters.Add(paramPrecoUnitario);
                updateCmd.Parameters.Add(paramId);

                updateCmd.ExecuteNonQuery();

            } catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        //delete usando Ado.Net
        public void Remover(Produto p)
        {
            try
            {
                var deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM Produtos WHERE Id = @id";

                var paramId = new SqlParameter("id", p.Id);
                deleteCmd.Parameters.Add(paramId);

                deleteCmd.ExecuteNonQuery();

            } catch(SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        //select usando Ado.Net
        public IList<Produto> Produtos()
        {
            var lista = new List<Produto>();

            var selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Produtos";

            var resultado = selectCmd.ExecuteReader();
            while (resultado.Read())
            {
                Produto p = new Produto();
                p.Id = Convert.ToInt32(resultado["Id"]);
                p.Nome = Convert.ToString(resultado["Nome"]);
                p.Categoria = Convert.ToString(resultado["Categoria"]);
                p.PrecoUnitario = Convert.ToDouble(resultado["PrecoUnitario"]);
                lista.Add(p);
            }
            resultado.Close();

            return lista;
        }
    }
}