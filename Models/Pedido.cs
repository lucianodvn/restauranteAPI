using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.API.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        [Required]
        public int QuantidadeSolicitada { get; set; }

        [Required]
        public string? NomeDoProduto { get; set; }

        [ForeignKey("Mesa")]
        public int IdMesa { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
    }
}

