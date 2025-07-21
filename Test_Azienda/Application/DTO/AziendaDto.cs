using System.ComponentModel.DataAnnotations;
using Test_Azienda1.Utilities.Attributes;

namespace Test_Azienda.Application.DTO
{
    public class AziendaDto
    {
        public AziendaDto() { }

        [Required]
        public int IDAzienda { get; set; }

        [Required]
        [AttributeCF(ErrorMessage = "Codice Fiscale non valido")]
        public string CodiceFiscale { get; set; }

        [Required]
        [AttributeIVA(ErrorMessage = "Partita Iva non valida")]
        public string PartitaIva { get; set; }

        public string? Descrizione { get; set; }

        [Required]
        public decimal Capitale { get; set; }

        public int IDRuolo { get; set; }

        [Required]
        public DateOnly DataInizioAtt { get; set; }

        public DateOnly? DataFineAtt { get; set; }
    }

    public class AziendaCancellazioneDto
    {
        public AziendaCancellazioneDto() { }

        [Required]
        public int IDAzienda { get; set; }

        public DateTime? DataCancellazione { get; set; }
    }

}