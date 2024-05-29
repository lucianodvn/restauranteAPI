﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.API.DTOs
{
    public class ProdutoDTO
    {
        public int IdProduto { get; set; }

        [Required]
        [StringLength(100)]
        public string? NomeDoProduto { get; set; }

        [Required]
        [StringLength(50)]
        public string? Descricao { get; set; }

        [Required]
        public double Preco { get; set; }

        [Required]
        public int IdUsuario { get; set; }
    }
}

