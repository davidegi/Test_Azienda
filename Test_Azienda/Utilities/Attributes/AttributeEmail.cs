using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Test_Azienda1.Utilities.Attributes
{
    public class AttributeEmail : ValidationAttribute
    {
        private const string Pattern_Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) // L'obbligatorietà è gestita con [Required]
                return ValidationResult.Success;
            if (value is not string email)
                return new ValidationResult("L'Email è obbligatoria");
            if (!Regex.IsMatch(email, Pattern_Email))
                return new ValidationResult("L'email non è corretta. Riprovare.");
            return ValidationResult.Success;
        }
    }
}
