using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurante.API.DTOs;
using Restaurante.API.Models;
using Restaurante.API.Repositories.Interface;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProdutoController : ControllerBase
    {
        private readonly IRepository<Produto> _repository;
        private readonly IMapper _mapper;

        public ProdutoController(IRepository<Produto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("consultarpodutos")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> ConsultarProdutos()
        {
            var response = await _repository.GetAll();
            if (response is null)
            {
                return BadRequest("Nenhum produto encontrado.");
            }

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(response);

            return Ok(produtosDto);
        }

        [HttpGet("consultarproduto/id")]
        public async Task<ActionResult<Produto>> ConsultarProduto(int id)
        {
            var response = await _repository.Get(p => p.IdProduto == id);

            if (response is null)
            {
                return BadRequest("Nenhum produto encontrado.");
            }

            var produtoDto = _mapper.Map<ProdutoDTO>(response);

            return Ok(produtoDto);
        }

        [HttpPost("cadastrarproduto")]
        public async Task<ActionResult> CadastrarProduto(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            _repository.Salvar(produto);
            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao salvar produto.");
            }

            return Ok("Produto cadastrado com sucesso.");
        }

        [HttpPut("alterarproduto")]
        public async Task<ActionResult> AlterarProduto(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            _repository.Update(produto);

            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao alterar produto.");
            }

            return Ok("Produto alterado com sucesso.");
        }

        [HttpDelete("deletarproduto/id")]
        public async Task<ActionResult> ExcluirProduto(int id)
        {
            var response = await _repository.Get(p => p.IdProduto == id);

            if (response is null)
            {
                return BadRequest("Produto não encontrado.");
            }

            _repository.Delete(response);
            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao excluir produto.");
            }

            return Ok("Produto excluído com sucesso.");
        }
    }
}

