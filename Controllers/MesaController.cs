using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurante.API.DTOs;
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
        private readonly IMapper _mapper;

        public MesaController(IRepository<Mesa> repositiry, IMapper mapper)
        {
            _repositiry = repositiry;
            _mapper = mapper;
        }

        [HttpGet("consutarmesas")]
        public async Task<ActionResult<IEnumerable<MesaDTO>>> ConsultarMesas()
        {
            var response = await _repositiry.GetAll();

            if (response is null || !response.Any())
            {
                return BadRequest("Mesa não encontrada.");
            }

            var mesaDTO = _mapper.Map<IEnumerable<MesaDTO>>(response);

            return Ok(mesaDTO);
        }

        [HttpGet("consultarmesa/id")]
        public async Task<ActionResult<MesaDTO>> ConsutarMesa(int id)
        {
            var response = await _repositiry.Get(m => m.IdMesa == id);

            if (response is null)
            {
                return BadRequest("Mesa não encontrada.");
            }

            var mesaDTO = _mapper.Map<MesaDTO>(response);

            return Ok(mesaDTO);
        }

        [HttpPost("cadastrarmesa")]
        public async Task<ActionResult> CadastrarMesa(MesaDTO mesaDto)
        {
            var mesa = _mapper.Map<Mesa>(mesaDto);

            _repositiry.Salvar(mesa);
            bool sucesso = await _repositiry.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro cadastrar mesa.");
            }

            return Ok("Mesa cadastrada com sucesso");
        }

        [HttpPut("alterarmesa")]
        public async Task<ActionResult> AlterarMesa(MesaDTO mesaDto)
        {
            var mesa = _mapper.Map<Mesa>(mesaDto);

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

