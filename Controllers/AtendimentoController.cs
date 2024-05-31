using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Interfaces;
using Restaurante.Domain.DTOs;
using Restaurante.Domain.Entities;
using Restaurante.Domain.Interfaces;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class AtendimentoController : Controller
    {
        private readonly IServiceGenerico<RestauranteApp> _service;
        private readonly IMapper _mapper;

        public AtendimentoController(IServiceGenerico<RestauranteApp> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("consultaratendimentos")]
        public async Task<ActionResult<IEnumerable<AtendimentoDTO>>> ConsultarAtendimento()
        {
            var response = await _service.GetAll();

            if (response is null)
            {
                return BadRequest("Não há nenhum atendimento.");
            }

            return Ok(response);
        }

        [HttpGet("consultaratendimento/id")]
        public async Task<ActionResult> ConsultarAtendimento(int id)
        {
            var response = await _service.Get(a => a.Id == id);

            if (response is null)
            {
                return BadRequest("Atendimento não encontrado.");
            }

            return Ok(response);
        }

        [HttpPost("cadastraratendimento")]
        public async Task<ActionResult> CadastrarAtendimento(AtendimentoDTO atendimentoDto)
        {
            var restauranteApp = _mapper.Map<RestauranteApp>(atendimentoDto);

            await _service.Salvar(restauranteApp);

            return Ok($"Atendimento salvo com sucesso. /n {restauranteApp}");
        }

        [HttpPut("alteraratendimento")]
        public async Task<ActionResult> AlterarAtendimento(AtendimentoDTO atendimentoDto)
        {
            var restauranteApp = _mapper.Map<RestauranteApp>(atendimentoDto);

            await _service.Update(restauranteApp);

            return Ok($"Atendimento alterado com sucesso. /n {restauranteApp}");
        }

        [HttpDelete("deletaratendimento/id")]
        public async Task<ActionResult> ExcluirAtendimento(int id)
        {
            var response = await _service.Get(a => a.Id == id);

            if (response is null)
            {
                return BadRequest("Atendimento não encontrado.");
            }

            _service.Delete(response);

            return Ok($"Atendimento excluído com sucesso. /n {response}");
        }
    }
}

