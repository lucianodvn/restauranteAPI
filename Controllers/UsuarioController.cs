using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Models;
using Restaurante.API.Repositories.Interface;

namespace Restaurante.API.Controllers
{
    [Route("controller")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IRepository<Usuario> _repository;

        public UsuarioController(IRepository<Usuario> repository)
        {
            _repository = repository;
        }

        [HttpGet("consultarusuario/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Usuario>> ConsultarUsuario(int id)
        {
            var response = await _repository.Get(u => u.IdUsuario == id);

            if (response is null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(response);
        }

        [HttpGet("consultarusuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Usuario>>> ConsultarListaDeUsuario()
        {
            var response = await _repository.GetAll();

            if (response is null)
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            return Ok(response);
        }

        [HttpPost("cadastrarusuario")]
        public async Task<ActionResult> CadastroUsuario([FromBody] Usuario usuario)
        {
            _repository.Salvar(usuario);
            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao salvar usuário.");
            }

            return Ok($"{200}: Usuário cadastrado com sucesso.");
        }

        [HttpPut("alterarusuario")]
        public async Task<ActionResult> AlterarUsuario(Usuario usuario)
        {
            _repository.Update(usuario);
            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao alterar usuário.");
            }

            return Ok($"{200}: Usuário alterado com sucesso.");
        }

        [HttpDelete("deleltarusuario/id")]
        public async Task<ActionResult> DeletarUsuario(int id)
        {
            var response = await _repository.Get(u => u.IdUsuario == id);

            if (response is null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            _repository.Delete(response);
            bool sucesso = await _repository.SalvarAleracoes();

            if (!sucesso)
            {
                return BadRequest("Erro ao excluir usuário.");
            }

            return Ok($"{200}: Usuário excluído com sucesso.");
        }
    }
}

