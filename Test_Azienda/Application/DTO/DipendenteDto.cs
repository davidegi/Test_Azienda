using System.ComponentModel.DataAnnotations;
using Test_Azienda1.Utilities.Attributes;

namespace Test_Azienda.Application.DTO
{
    public class DipendenteDto
    {
        [Required]
        public int IDDipendente { get; set; }

        [Required]
        [AttributeCF(ErrorMessage = "Codice Fiscale non valido")]
        public string? CodiceFiscale { get; set; }

        [Required]
        [AttributeIVA(ErrorMessage = "Partita Iva non valida")]
        public string? PartitaIva { get; set; }

        [Required]
        public string? Professione { get; set; }

        [Required]
        public string? Qualifica { get; set; }
    }
}
