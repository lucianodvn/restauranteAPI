using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Models;
using Restaurante.API.Repositories.Interface;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProdutoController : ControllerBase
    {
        private readonly IRepository<Produto> _repository;

        public ProdutoController(IRepository<Produto> repository)
        {
            _repository = repository;
        }

        [HttpGet("consultarpodutos")]
        public async Task<ActionResult<IEnumerable<Produto>>> ConsultarProdutos()
        {
            var response = await _repository.GetAll();
            if (response is null)
            {
                return BadRequest("Nenhum produto encontrado.");
            }

            return Ok(response);
        }

        [HttpGet("consultarproduto/id")]
        public async Task<ActionResult<Produto>> ConsultarProduto(int id)
        {
            var response = await _repository.Get(p => p.IdProduto == id);

            if (response is null)
            {
                return BadRequest("Nenhum produto encontrado.");
            }

            return Ok(response);
        }

        [HttpPost("cadastrarproduto")]
        public async Task<ActionResult> CadastrarProduto(Produto produto)
        {
            _repository.Salvar(produto);
            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao salvar produto.");
            }

            return Ok("Produto cadastrado com sucesso.");
        }

        [HttpPut("alterarproduto")]
        public async Task<ActionResult> AlterarProduto(Produto produto)
        {
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

