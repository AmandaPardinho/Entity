﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class ProdutoDAO : IDisposable
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
        internal void Adicionar(Produto p)
        {
            try
            {
                var insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Produtos (Nome, Categoria, Preco) VALUES (@nome, @categoria, @preco)";

                var paramNome = new SqlParameter("nome", p.Nome);
                insertCmd.Parameters.Add(paramNome);

                var paramCategoria = new SqlParameter("categoria", p.Categoria);
                insertCmd.Parameters.Add(paramCategoria);

                var paramPreco = new SqlParameter("preco", p.Preco);
                insertCmd.Parameters.Add(paramPreco);

                insertCmd.ExecuteNonQuery();
            } catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        //update usando Ado.Net
        internal void Atualizar(Produto p)
        {
            try
            {
                var updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "UPDATE Produtos SET Nome = @nome, Categoria = @categoria, Preco = @preco WHERE Id = @id";

                var paramNome = new SqlParameter("nome", p.Nome);
                var paramCategoria = new SqlParameter("categoria", p.Categoria);
                var paramPreco = new SqlParameter("preco", p.Preco);
                var paramId = new SqlParameter("id", p.Id);
                updateCmd.Parameters.Add(paramNome);
                updateCmd.Parameters.Add(paramCategoria);
                updateCmd.Parameters.Add(paramPreco);
                updateCmd.Parameters.Add(paramId);

                updateCmd.ExecuteNonQuery();

            } catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        //delete usando Ado.Net
        internal void Remover(Produto p)
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
        internal IList<Produto> Produtos()
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
                p.Preco = Convert.ToDouble(resultado["Preco"]);
                lista.Add(p);
            }
            resultado.Close();

            return lista;
        }
    }
}