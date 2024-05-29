using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Models;
using Restaurante.API.Repositories.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class AtendimentoController : Controller
    {
        private readonly IRepository<RestauranteApp> _repository;

        public AtendimentoController(IRepository<RestauranteApp> repository)
        {
            _repository = repository;
        }

        [HttpGet("consultaratendimentos")]
        public async Task<ActionResult> ConsultarAtendimento()
        {
            var response = await _repository.GetAll();

            if (response is null)
            {
                return BadRequest("Não há nenhum atendimento.");
            }

            return Ok(response);
        }

        [HttpGet("consultaratendimento/id")]
        public async Task<ActionResult> ConsultarAtendimento(int id)
        {
            var response = await _repository.Get(a => a.Id == id);

            if (response is null)
            {
                return BadRequest("Atendimento não encontrado.");
            }

            return Ok(response);
        }

        [HttpPost("cadastraratendimento")]
        public async Task<ActionResult> CadastrarAtendimento(RestauranteApp restauranteApp)
        {
            _repository.Salvar(restauranteApp);

            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao salvar atendimento.");
            }

            return Ok("Atendimento salvo com sucesso.");
        }

        [HttpPut("alteraratendimento")]
        public async Task<ActionResult> AlterarAtendimento(RestauranteApp restauranteApp)
        {
            _repository.Update(restauranteApp);

            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao alterar atendimento.");
            }

            return Ok("Atendimento alterado com sucesso.");
        }

        [HttpDelete("deletaratendimento/id")]
        public async Task<ActionResult> ExcluirAtendimento(int id)
        {
            var response = await _repository.Get(a => a.Id == id);

            if (response is null)
            {
                return BadRequest("Atendimento não encontrado.");
            }

            _repository.Delete(response);

            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao excluir atendimento.");
            }

            return Ok("Atendimento excluído com sucesso.");
        }
    }
}

