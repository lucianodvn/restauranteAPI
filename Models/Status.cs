using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.API.Models
{
    public class Status
    {
        [Key]
        public int IdStatus { get; set; }

        [Required]
        [StringLength(20)]
        public string? TipoDeStatus { get; set; }
    }
}

