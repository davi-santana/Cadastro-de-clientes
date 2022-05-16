using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiCadastraClienteBancoDeDados.Utils
{
    public static class Validar
    {
        public static bool ValidarCpfj(string Cpfj)
        {
            if (new Caelum.Stella.CSharp.Validation.CPFValidator().IsValid(Cpfj))
                return true;
            if (new Caelum.Stella.CSharp.Validation.CNPJValidator().IsValid(Cpfj))
                return true;

            return false;
        }

        public static bool ValidarEmail(String email)
        {
            bool emailValido = false;

            //Expressão regular retirada de
            //https://msdn.microsoft.com/pt-br/library/01escwtf(v=vs.110).aspx
            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            try
            {
                emailValido = Regex.IsMatch(
                    email,
                    emailRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                emailValido = false;
            }

            return emailValido;
        }

        public static string ValidarCliente(Entidades.Cliente cliente)
        {
            if (String.IsNullOrEmpty(cliente.Cpfj))
                return "Cpfj Não foi Preenchido";

            if (Utils.Validar.ValidarCpfj(cliente.Cpfj) == false)
                return "Cpfj não é valido";

            if (String.IsNullOrEmpty(cliente.RazaoSocial))
                return "Razao Social Não preenchida";

            if (String.IsNullOrEmpty(cliente.Email))
                return " Email Não preenchido";

            //if (Decimal.(cliente.CapitalSocial))
            //    return "Cpfj Não foi Preenchido";

            if (cliente.CapitalSocial < 0)
            {
                return "valor invalido";
            }


            cliente.Telefone = SomenteNumero(cliente.Telefone);

            if (String.IsNullOrEmpty(cliente.Telefone))
            {
                cliente.Telefone = "00000000000";
            }

            if (SomenteNumero(cliente.Telefone).Length != 11)
            {
                return "tamanho do numero invalido";
            }

            if (cliente.Telefone == "00000000000")
            {
                cliente.Telefone = null;
            }

            return null;
        }

        public static string SomenteNumero(String telefone)
        {

            return String.Join("", System.Text.RegularExpressions.Regex.Split(telefone, @"[^\d]"));
        }

  

    
    }

}
