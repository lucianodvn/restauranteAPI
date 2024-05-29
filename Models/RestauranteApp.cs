using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.API.Models
{
    public class RestauranteApp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NumeroDoAtendimento { get; set; }

        [Required]
        public DateTime DataChegada { get; set; }

        public DateTime DataSaida { get; set; }

        [ForeignKey("Mesa")]
        public int IdMesa { get; set; }
    }
}

