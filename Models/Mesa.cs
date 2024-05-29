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

        [StringLength(20)]
        public string? StatusDaMesa { get; set; }
    }
}

