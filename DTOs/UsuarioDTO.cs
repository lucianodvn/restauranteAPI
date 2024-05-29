using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.API.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Usuário requerido.")]
        public string? NomeUsuario { get; set; }

        [Required(ErrorMessage = "Email requerido.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha requerido.")]
        public string? senha { get; set; }

        [Required]
        public bool PermissaoCadastrarProduto { get; set; }
    }
}

