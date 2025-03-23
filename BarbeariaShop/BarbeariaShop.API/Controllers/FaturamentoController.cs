using Baber.Control;
using Baber.Control.Filtro;
using Baber.Model.Entity;
using Baber.Model.Request.Registros;
using Baber.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Net.WebSockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarbeariaShop.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FaturamentoController : ControllerBase
    {
        private readonly RegraFaturamento _regraFaturamento;
        public FaturamentoController(RegraFaturamento regraFaturamento)
        {
            _regraFaturamento = regraFaturamento;
        }

        [HttpPost("Registrar")]
        [ProducesResponseType(typeof(List<ResponseRegistro>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Post([FromBody] Registros request)
        {
            try
            {
                var response = _regraFaturamento.Salvar(request);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ListaFaturamento")]
        [ProducesResponseType(typeof(List<Faturamento>), 200)]
        public IActionResult GetAll()
        {
            try
            {
                var Lista = _regraFaturamento.ListaRegistro();

                if (Lista == null || Lista.Count == 0)
                {
                    return NotFound();
                }
                return Ok(Lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ListaFaturamento_porID/ {id}")]
        [ProducesResponseType(typeof(List<Faturamento>), 200)]
        public IActionResult Get([FromRoute] int Id)
        {
            try
            {
                var Lista = _regraFaturamento.ListaRegistro(Id);
                return Ok(Lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Atualizar/ {id}")]
        [ProducesResponseType(typeof(List<Faturamento>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Update([FromRoute] int id, [FromBody] Registros request)
        {
            try
            {
                _regraFaturamento.Atualizar(request, id);
                return NoContent();


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                bool Deletado = _regraFaturamento.Deletar(id);
                if (Deletado == true)
                {
                    return NoContent();
                }

                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return NoContent();
            }
        }

    }
}
