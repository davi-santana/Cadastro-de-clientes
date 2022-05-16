using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiCadastraClienteBancoDeDados.Entidades;

namespace ApiCadastraClienteBancoDeDados.Conexoes
{
    public class Sql
    {
        private readonly SqlConnection _conexao;
        public Sql()
        {
            this._conexao = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB");
        }

        public void InsereProduto(Entidades.Cliente cliente)
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

                //            cmd.Parameters.AddWithValue("@surname",
                //String.IsNullOrEmpty(surname) ? DBNull.Value : surname);
            }

            finally
            {
                _conexao.Close();
            }
        }
        public List<Cliente> ListarClientes()
        {

            _conexao.Open();

            var clientes = new List<Cliente>();

            string sql = @"SELECT Cpfj, RazaoSocial, Email, CapitalSocial, Telefone FROM Cliente";

            using (var cmd = new SqlCommand(sql, _conexao))
            {

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var cliente = new Cliente();

                        cliente.Cpfj = rdr["Cpfj"].ToString();
                        cliente.RazaoSocial = rdr["RazaoSocial"].ToString();
                        cliente.Email = rdr["Email"].ToString();
                        cliente.Telefone = Utils.Conversoes.ConverterParaString.ConverterString(rdr["Telefone"].ToString());
                        cliente.CapitalSocial = Utils.Conversoes.ConverterDecimal.ConverterParaDecimal(rdr["CapitalSocial"]);

                        clientes.Add(cliente);
                    }
                }

            }

            _conexao.Close();

            return clientes;
        }

        public Cliente DadosCliente(string Cpfj)
        {

            try
            {
                _conexao.Open();

                //Cliente DadoCliente = null;

                string sql = @"SELECT Cpfj, RazaoSocial, Email, CapitalSocial, Telefone FROM Cliente WHERE Cpfj = @cpfj";

                using (var cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("cpfj", Cpfj);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            return new Cliente()
                            {
                                Cpfj = rdr["Cpfj"].ToString(),
                                RazaoSocial = rdr["RazaoSocial"].ToString(),
                                Email = rdr["Email"].ToString(),
                                Telefone = Utils.Conversoes.ConverterParaString.ConverterString(rdr["Telefone"].ToString()),
                                CapitalSocial = Utils.Conversoes.ConverterDecimal.ConverterParaDecimal(rdr["CapitalSocial"])
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

        }

        public void EditarCliente(Cliente cliente)
        {

            try
            {

                _conexao.Open();

                string sql = @"UPDATE Cliente SET 
                                RazaoSocial = @razaoSocial,
                                Email = @email, 
                                CapitalSocial = @capitalSocial,
                                Telefone = @telefone
                                WHERE Cpfj = @Cpfj;";

                using (SqlCommand cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("@razaoSocial", cliente.RazaoSocial);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@capitalSocial", cliente.CapitalSocial);
                    cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    cmd.Parameters.AddWithValue("@cpfj", cliente.Cpfj);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }

        }

        public int DeletarCliente(string Cpfj)
        {
            try
            {
                _conexao.Open();


                string sql = @"DELETE from Cliente where Cpfj = @cpfj;";

                using (var cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("cpfj", Cpfj);
                    return cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        public bool CpfjJaExiste(string Cpfj)
        {

            try
            {
                _conexao.Open();

                string sql = @"SELECT Cpfj FROM Cliente WHERE Cpfj = @Cpfj";

                using (SqlCommand cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("cpfj", Cpfj);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {

                        return true;

                    }
                }
            }

            finally
            {
                _conexao.Close();
            }
            return false;
        }
    }
}

 
