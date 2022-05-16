using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClientesBaseCsv.Entidades
{
    class Cliente
    {
        public string Cpfj { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public decimal CapitalSocial { get; set; }
        public string Telefone { get; set; }
    }
}
