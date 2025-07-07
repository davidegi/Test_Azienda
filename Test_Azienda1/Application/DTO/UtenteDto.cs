using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Test_Azienda1.Utilities.Attributes;

namespace Test_Azienda1.Application.DTO
{
    public class UtenteDto
    {
        [Required]
        public int IDUtente { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [AttributeEmail(ErrorMessage = "Email non valida")]
        public string? Email { get; set; }

        [Required]
        [JsonIgnore]
        [AttributePassword(ErrorMessage = "Password non valida")]
        public string? Password { get; set; }
    }
}
