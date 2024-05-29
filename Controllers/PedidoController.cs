using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Models;
using Restaurante.API.Repositories.Interface;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PedidoController : ControllerBase
    {
        private readonly IRepository<Pedido> _repository;

        public PedidoController(IRepository<Pedido> repository)
        {
            _repository = repository;
        }

        [HttpGet("consultarpedidos")]
        public async Task<ActionResult<IEnumerable<Pedido>>> ConsultarPedidos()
        {
            var response = await _repository.GetAll();

            if (response is null)
            {
                return BadRequest("Nenhum pedido não encontrado.");
            }

            return Ok(response);
        }

        [HttpGet("consultarpedido/id")]
        public async Task<ActionResult<IEnumerable<Pedido>>> ConsultarPedidos(int id)
        {
            var response = await _repository.Get(p => p.IdPedido == id);

            if (response is null)
            {
                return BadRequest("Nenhum pedido não encontrado.");
            }

            return Ok(response);
        }

        [HttpPost("cadastrarpedido")]
        public async Task<ActionResult> CadastrarPedido(Pedido pedido)
        {
            _repository.Salvar(pedido);

            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao salvar pedido.");
            }

            return Ok("Pedido salvo com sucesso.");
        }

        [HttpPut("alterarpedido")]
        public async Task<ActionResult> AlterarPedido(Pedido pedido)
        {
            _repository.Update(pedido);

            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao alterar pedido.");
            }

            return Ok("Pedido alterado com sucesso.");
        }

        [HttpDelete("deletarpedido/id")]
        public async Task<ActionResult> ExcluirPedido(int id)
        {
            var response = await _repository.Get(p => p.IdPedido == id);

            if (response is null)
            {
                return BadRequest("Nenhum pedido não encontrado.");
            }

            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao excluir pedido.");
            }

            return Ok("Pedido excluído com sucesso.");
        }
    }
}

