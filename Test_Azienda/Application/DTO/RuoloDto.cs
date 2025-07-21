using System.ComponentModel.DataAnnotations;

namespace Test_Azienda.Application.DTO
{
    public class RuoloDto
    {
        [Required]
        public int IDRuolo { get; set; }

        [Required]
        public string? Tipo { get; set; }

        public string? Descrizione { get; set; }
    }
}
