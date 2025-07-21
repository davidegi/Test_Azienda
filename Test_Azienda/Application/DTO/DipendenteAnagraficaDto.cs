using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Azienda.Application.DTO
{
    public class DipendenteAnagraficaDto
    {
        [Required]
        public int IDDipendenteAnag { get; set; }

        [Required]
        [Column("nome")]
        public string? Nome { get; set; }

        [Required]
        [Column("cognome")]
        public string? Cognome { get; set; }

        [Required]
        public string? LuogoNascita { get; set; }

        [Required]
        public DateOnly? DataNascita { get; set; }
    }
}
