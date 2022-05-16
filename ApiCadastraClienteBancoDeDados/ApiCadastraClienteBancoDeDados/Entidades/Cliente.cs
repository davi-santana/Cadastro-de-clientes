using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastraClienteBancoDeDados.Entidades
{
    public class Cliente
    {
        public string Cpfj { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public decimal? CapitalSocial { get; set; }
        public string Telefone { get; set; }
    }
}
