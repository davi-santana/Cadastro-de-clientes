using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiCadastraClienteBancoDeDados.Entidades;
using ApiCadastraClienteBancoDeDados.Conexoes;
using Caelum.Stella.CSharp.Validation;
using Microsoft.AspNetCore.Http;

namespace ApiCadastraClienteBancoDeDados.Controllers
{
    public class ExecutaApiController : Controller
    {
        [HttpPost]
        [Route("Cliente")]

        public ActionResult InsereProduto([FromBody] Cliente cliente)
        {
            var sql = new Sql();
            try
            {
                #region VALIDAÇÕES



                if (Utils.Validar.ValidarCliente(cliente) != null)
                    return StatusCode(400, "ocorreu um erro interno");

                if (cliente.Cpfj.Contains(".") || cliente.Cpfj.Contains("-") || cliente.Cpfj.Contains("/"))
                {
                    cliente.Cpfj = cliente.Cpfj.Replace(".", "");
                    cliente.Cpfj = cliente.Cpfj.Replace("-", "");
                    cliente.Cpfj = cliente.Cpfj.Replace("/", "");
                }

                if (Utils.Validar.ValidarEmail(cliente.Email) == false)
                    return StatusCode(400, "Email não e valido");

                #endregion



                if (!CpfjJaExiste(cliente.Cpfj))
                {
                    sql.InsereProduto(cliente);
                    return Ok();
                }

                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um Erro Interno");
            }

        }
        private bool CpfjJaExiste(string Cpfj)
        {
            Sql sql = new Sql();
            return sql.CpfjJaExiste(Cpfj);
        }

        [HttpGet]
        [Route("Cliente")]

        public ActionResult ListarClientes()
        {
            Sql sql = new Sql();
            try
            {
                return Ok(sql.ListarClientes());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro Interno");
            }
        }

        [HttpGet]
        [Route("cliente/{Cpfj}")]
        public ActionResult Dadosliente(string Cpfj)
        {
            try
            {

                if (!String.IsNullOrEmpty(Cpfj))
                {

                    Sql sql = new Sql();

                    var dadosCliente = sql.DadosCliente(Cpfj);

                    if (dadosCliente != null)
                        return Ok(dadosCliente);
                    else
                        return StatusCode(404, "Cpfj não encontrado");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um Erro");
            }

            return Ok();
        }

        [HttpPut]
        [Route("Cliente/{Cpfj}")]

        public ActionResult EditarCliente(string Cpfj, [FromBody] Cliente cliente)
        {
            #region Validações
            if (Utils.Validar.ValidarCliente(cliente) != null)
            {
                return StatusCode(400, "ocorreu um erro interno");
            }

            if (Utils.Validar.ValidarEmail(cliente.Email) == false)
                    return StatusCode(400, "Email não e valido");

            #endregion

            cliente.Cpfj = Cpfj;
            Sql sql = new Sql();
            sql.EditarCliente(cliente);
            return Ok(cliente);
        }


        [HttpDelete]
        [Route("cliente/{Cpfj}")]
        public ActionResult DeletarCliente(string Cpfj)
        {
            try
            {

                if (!String.IsNullOrEmpty(Cpfj))
                {

                    Sql sql = new Sql();

                    var linhaAfetada = sql.DeletarCliente(Cpfj);

                    if (linhaAfetada == 1)
                        return Ok();
                    else
                        return StatusCode(404, "Cpfj não encontrado");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um Erro");
            }

            return Ok();
        }
    }

}
