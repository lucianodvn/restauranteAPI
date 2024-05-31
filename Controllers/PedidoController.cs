using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Interfaces;
using Restaurante.Domain.DTOs;
using Restaurante.Domain.Entities;
using Restaurante.Domain.Interfaces;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PedidoController : ControllerBase
    {
        private readonly IServiceGenerico<Pedido> _service;
        private readonly IMapper _mapper;

        public PedidoController(IServiceGenerico<Pedido> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("consultarpedidos")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> ConsultarPedidos()
        {
            var response = await _service.GetAll();

            if (response is null)
            {
                return BadRequest("Nenhum pedido não encontrado.");
            }

            return Ok(response);
        }

        [HttpGet("consultarpedido/id")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> ConsultarPedidos(int id)
        {
            var response = await _service.Get(p => p.IdPedido == id);

            if (response is null)
            {
                return BadRequest("Nenhum pedido não encontrado.");
            }

            return Ok(response);
        }

        [HttpPost("cadastrarpedido")]
        public async Task<ActionResult> CadastrarPedido(PedidoDTO pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);

            await _service.Salvar(pedido);

            return Ok($"Pedido salvo com sucesso. /n {pedido}");
        }

        [HttpPut("alterarpedido")]
        public async Task<ActionResult> AlterarPedido(PedidoDTO pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);

            await _service.Update(pedido);

            return Ok($"Pedido alterado com sucesso. /n {pedido}");
        }

        [HttpDelete("deletarpedido/id")]
        public async Task<ActionResult> ExcluirPedido(int id)
        {
            var response = await _service.Get(p => p.IdPedido == id);

            if (response is null)
            {
                return BadRequest("Nenhum pedido não encontrado.");
            }

            _service.Delete(response);

            return Ok($"Pedido excluído com sucesso. /n {response}");
        }
    }
}

