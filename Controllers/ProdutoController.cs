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
    public class ProdutoController : ControllerBase
    {
        private readonly IServiceGenerico<Produto> _service;
        private readonly IMapper _mapper;

        public ProdutoController(IServiceGenerico<Produto> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("consultarpodutos")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> ConsultarProdutos()
        {
            var response = await _service.GetAll();
            if (response is null)
            {
                return BadRequest("Nenhum produto encontrado.");
            }

            return Ok(response);
        }

        [HttpGet("consultarproduto/id")]
        public async Task<ActionResult<Produto>> ConsultarProduto(int id)
        {
            var response = await _service.Get(p => p.IdProduto == id);

            if (response is null)
            {
                return BadRequest("Nenhum produto encontrado.");
            }

            return Ok(response);
        }

        [HttpPost("cadastrarproduto")]
        public async Task<ActionResult> CadastrarProduto(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            _service.Salvar(produto);


            return Ok($"Produto cadastrado com sucesso./n {produto}");
        }

        [HttpPut("alterarproduto")]
        public async Task<ActionResult> AlterarProduto(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            _service.Update(produto);

            return Ok($"Produto alterado com sucesso. /n {produto}");
        }

        [HttpDelete("deletarproduto/id")]
        public async Task<ActionResult> ExcluirProduto(int id)
        {
            var response = await _service.Get(p => p.IdProduto == id);

            if (response is null)
            {
                return BadRequest("Produto não encontrado.");
            }

            _service.Delete(response);

            return Ok($"Produto excluído com sucesso. /n {response}");
        }
    }
}

