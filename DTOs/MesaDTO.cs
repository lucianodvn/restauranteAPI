using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.API.DTOs
{
    public class MesaDTO
    {
        public int IdMesa { get; set; }

        [Required]
        [StringLength(10)]
        public string? NumeroDaMesa { get; set; }

        [Required]
        [StringLength(20)]
        public string? StatusDaMesa { get; set; }
    }
}

