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
    public class MesaController : Controller
    {
        private readonly IServiceGenerico<Mesa> _service;
        private readonly IMapper _mapper;

        public MesaController(IServiceGenerico<Mesa> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("consutarmesas")]
        public async Task<ActionResult<IEnumerable<MesaDTO>>> ConsultarMesas()
        {
            var response = await _service.GetAll();

            if (response is null || !response.Any())
            {
                return BadRequest("Mesa não encontrada.");
            }

            return Ok(response);
        }

        [HttpGet("consultarmesa/id")]
        public async Task<ActionResult<MesaDTO>> ConsutarMesa(int id)
        {
            var response = await _service.Get(m => m.IdMesa == id);

            if (response is null)
            {
                return BadRequest("Mesa não encontrada.");
            }

            return Ok(response);
        }

        [HttpPost("cadastrarmesa")]
        public async Task<ActionResult> CadastrarMesa(MesaDTO mesaDto)
        {
            var mesa = _mapper.Map<Mesa>(mesaDto);

            _service.Salvar(mesa);

            return Ok($"Mesa cadastrada com sucesso. /n {mesa}");
        }

        [HttpPut("alterarmesa")]
        public async Task<ActionResult> AlterarMesa(MesaDTO mesaDto)
        {
            var mesa = _mapper.Map<Mesa>(mesaDto);

            _service.Update(mesa);

            return Ok($"Mesa cadastrada com sucesso. /n {mesa}");
        }

        [HttpDelete("deletarmesa/id")]
        public async Task<ActionResult> ExcluirMesa(int id)
        {
            var response = await _service.Get(m => m.IdMesa == id);

            if (response is null)
            {
                return BadRequest("Mesa não encontrada.");
            }

            _service.Delete(response);

            return Ok($"Mesa excluída com sucesso. /n {response}");
        }
    }
}

