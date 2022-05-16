using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastraClienteBancoDeDados.Utils.Conversoes
{
    public static class ConverterDecimal
    {
        public static decimal? ConverterParaDecimal(object valor)
        {
            if (valor != DBNull.Value)
            {
                return Convert.ToDecimal(valor);
            }

            else
            {
                return null;
            }
                
        }
    }
}
