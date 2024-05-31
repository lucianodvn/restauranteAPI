using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Interfaces;
using Restaurante.Domain.DTOs;
using Restaurante.Domain.Entities;
using Restaurante.Domain.Interfaces;

namespace Restaurante.API.Controllers
{
    [Route("controller")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServiceGenerico<Usuario> _service;
        private readonly IMapper _mapper;

        public UsuarioController(IServiceGenerico<Usuario> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("consultarusuario/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioDTO>> ConsultarUsuario(int id)
        {
            var response = await _service.Get(u => u.IdUsuario == id);

            if (response is null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(response);
        }

        [HttpGet("consultarusuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> ConsultarListaDeUsuario()
        {
            var response = await _service.GetAll();

            if (response is null)
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            return Ok(response);
        }

        [HttpPost("cadastrarusuario")]
        public async Task<ActionResult> CadastroUsuario([FromBody] UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);

            await _service.Salvar(usuario);

            return Ok($"{200}: Usuário cadastrado com sucesso.");
        }

        [HttpPut("alterarusuario")]
        public async Task<ActionResult> AlterarUsuario(UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);

            await _service.Update(usuario);

            return Ok($"{200}: Usuário alterado com sucesso.");
        }

        [HttpDelete("deleltarusuario/id")]
        public async Task<ActionResult> DeletarUsuario(int id)
        {
            var response = await _service.Get(u => u.IdUsuario == id);

            if (response is null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            _service.Delete(response);

            return Ok($"{200}: Usuário excluído com sucesso.");
        }
    }
}

