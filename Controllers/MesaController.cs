using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Models;
using Restaurante.API.Repositories;
using Restaurante.API.Repositories.Interface;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class MesaController : Controller
    {
        private readonly IRepository<Mesa> _repositiry;

        public MesaController(IRepository<Mesa> repositiry)
        {
            _repositiry = repositiry;
        }

        [HttpGet("consutarmesas")]
        public async Task<ActionResult<IEnumerable<Mesa>>> ConsultarMesas()
        {
            var response = await _repositiry.GetAll();

            if (response is null)
            {
                return BadRequest("Mesa não encontrada.");
            }

            return Ok(response);
        }

        [HttpGet("consultarmesa/id")]
        public async Task<ActionResult<Mesa>> ConsutarMesa(int id)
        {
            var response = await _repositiry.Get(m => m.IdMesa == id);

            if (response is null)
            {
                return BadRequest("Mesa não encontrada.");
            }

            return Ok(response);
        }

        [HttpPost("cadastrarmesa")]
        public async Task<ActionResult> CadastrarMesa(Mesa mesa)
        {
            _repositiry.Salvar(mesa);
            bool sucesso = await _repositiry.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro cadastrar mesa.");
            }

            return Ok("Mesa cadastrada com sucesso");
        }

        [HttpPut("alterarmesa")]
        public async Task<ActionResult> AlterarMesa(Mesa mesa)
        {
            _repositiry.Update(mesa);

            bool sucesso = await _repositiry.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro cadastrar mesa.");
            }

            return Ok("Mesa cadastrada com sucesso.");
        }

        [HttpDelete("deletarmesa/id")]
        public async Task<ActionResult> ExcluirMesa(int id)
        {
            var response = await _repositiry.Get(m => m.IdMesa == id);

            if (response is null)
            {
                return BadRequest("Mesa não encontrada.");
            }

            _repositiry.Delete(response);
            bool sucesso = await _repositiry.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro excluir mesa.");
            }

            return Ok("Mesa excluída com sucesso.");
        }
    }
}

