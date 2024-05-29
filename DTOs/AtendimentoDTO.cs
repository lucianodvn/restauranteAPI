using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.API.DTOs
{
    public class AtendimentoDTO
    {
        public int Id { get; set; }

        [Required]
        public int NumeroDoAtendimento { get; set; }

        [Required]
        public DateTime DataChegada { get; set; }

        public DateTime? DataSaida { get; set; }

        [Required]
        public int IdMesa { get; set; }

        public double ValorTotal { get; set; }

        [Required]
        public int IdPedido { get; set; }
    }
}

