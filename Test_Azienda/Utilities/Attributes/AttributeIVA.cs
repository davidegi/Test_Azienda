using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Test_Azienda.Utilities.Attributes
{
    public class AttributeIVA : ValidationAttribute
    {
        private const string Pattern_IVA = @"^(?i:IT)?\d{11}$";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) // L'obbligatorietà è gestita con [Required]
                return ValidationResult.Success;
            if (value is not string iva)
                return new ValidationResult("La partita IVA è obbligatoria.");
            if (!Regex.IsMatch(iva, Pattern_IVA))
                return new ValidationResult("partita IVA non valida");
            return ValidationResult.Success;
        }
    }
}
