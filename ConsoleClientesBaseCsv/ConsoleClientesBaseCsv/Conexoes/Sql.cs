using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleClientesBaseCsv.Conexoes
{
    class Sql
    {
        private readonly SqlConnection _conexao;

        public Sql()
        {
            this._conexao = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB");
        }
        public void InserirBaseDeDados(Entidades.Cliente cliente)
        {
            try
            {
                _conexao.Open();

                string sql = @"INSERT INTO Cliente
                                (Cpfj, RazaoSocial, Email, CapitalSocial, Telefone)
                               VALUES
                                (@cpfj, @razaoSocial, @email, @capitalSocial, @telefone);";

                using (SqlCommand cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("cpfj", cliente.Cpfj);
                    cmd.Parameters.AddWithValue("razaoSocial", cliente.RazaoSocial);
                    cmd.Parameters.AddWithValue("email", cliente.Email);
                    cmd.Parameters.AddWithValue("capitalSocial", cliente.CapitalSocial);
                    cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
    }
}
