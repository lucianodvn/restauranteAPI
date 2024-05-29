using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurante.API.DTOs;
using Restaurante.API.Models;
using Restaurante.API.Repositories.Interface;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PedidoController : ControllerBase
    {
        private readonly IRepository<Pedido> _repository;
        private readonly IMapper _mapper;

        public PedidoController(IRepository<Pedido> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("consultarpedidos")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> ConsultarPedidos()
        {
            var response = await _repository.GetAll();

            if (response is null)
            {
                return BadRequest("Nenhum pedido não encontrado.");
            }

            var pedidosDto = _mapper.Map<IEnumerable<PedidoDTO>>(response);

            return Ok(pedidosDto);
        }

        [HttpGet("consultarpedido/id")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> ConsultarPedidos(int id)
        {
            var response = await _repository.Get(p => p.IdPedido == id);

            if (response is null)
            {
                return BadRequest("Nenhum pedido não encontrado.");
            }

            var pedidoDto = _mapper.Map<PedidoDTO>(response);

            return Ok(pedidoDto);
        }

        [HttpPost("cadastrarpedido")]
        public async Task<ActionResult> CadastrarPedido(PedidoDTO pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);

            _repository.Salvar(pedido);

            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao salvar pedido.");
            }

            return Ok("Pedido salvo com sucesso.");
        }

        [HttpPut("alterarpedido")]
        public async Task<ActionResult> AlterarPedido(PedidoDTO pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);

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

            _repository.Delete(response);

            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao excluir pedido.");
            }

            return Ok("Pedido excluído com sucesso.");
        }
    }
}

