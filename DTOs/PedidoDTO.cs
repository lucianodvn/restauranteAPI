using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.API.DTOs
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }

        [Required]
        public int QuantidadeSolicitada { get; set; }

        [Required]
        public string? NomeDoProduto { get; set; }

        [Required]
        public int IdMesa { get; set; }

        [Required]
        public int IdProduto { get; set; }
    }
}

