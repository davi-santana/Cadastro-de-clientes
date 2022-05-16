using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastraClienteBancoDeDados.Utils.Conversoes
{
    public class ConverterParaString
    {
        public static string ConverterString(object valor)
        {
            if (valor != DBNull.Value)
                return Convert.ToString(valor);
            else
                return null;
        }
    }
}
