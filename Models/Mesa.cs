using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.API.Models
{
    public class Mesa
    {
        [Key]
        public int IdMesa { get; set; }

        [Required]
        [StringLength(10)]
        public string? NumeroDaMesa { get; set; }

        [Required]
        public int QuantidaDePessoas { get; set; }

        [Required]
        [StringLength(100)]
        public string? ProdutoConsumido { get; set; }

        [Required]
        public double ValorTotal { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }

        [ForeignKey("Status")]
        public int IdStatus { get; set; }
    }
}

