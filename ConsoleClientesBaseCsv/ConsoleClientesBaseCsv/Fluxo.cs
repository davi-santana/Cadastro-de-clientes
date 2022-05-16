using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleClientesBaseCsv
{
    class Fluxo
    {
        public void ImportaçaoDeCliente()
        {

            var clientes = LerArquivoDeTexto();
            var sql = new Conexoes.Sql();
            foreach (var cliente in clientes)
                ImportacaoBaseDeDados(cliente, sql);
        }
        private void ImportacaoBaseDeDados(Entidades.Cliente cliente, Conexoes.Sql sql)
        {
            sql.InserirBaseDeDados(cliente);
        }

        private List<Entidades.Cliente> LerArquivoDeTexto()
        {
            var clientes = new List<Entidades.Cliente>();
            string conteudoArquivo = File.ReadAllText(@"C:\DadosCsv\Clientes.csv");
            string[] LinhasArquivo = conteudoArquivo.Split(Environment.NewLine); //essa linha eu não entendi

            int contador = 0; //criando ele so pra pular a primeira linha das informações

            int contador2 = 0;
            foreach (var linhaArquivo in LinhasArquivo)
            {
                contador++;
                if (contador == 1)
                {
                    continue;
                }


                string[] colunasLinha = linhaArquivo.Split(","); //quebra linha a partir do que esta dentro do " "

                var cliente = new Entidades.Cliente();



                if (linhaArquivo == "")
                {
                    contador2++;
                    continue;
                }

                cliente.RazaoSocial = colunasLinha[0];
                cliente.Email = colunasLinha[1];
                cliente.CapitalSocial = Convert.ToDecimal(colunasLinha[2].Replace(".", ",").Replace("$", ""));
                cliente.Telefone = colunasLinha[3];
                cliente.Cpfj = colunasLinha[4].Replace(".","").Replace("/","").Replace("-","");

                clientes.Add(cliente);

            }

            return clientes;
        }
    }
}
