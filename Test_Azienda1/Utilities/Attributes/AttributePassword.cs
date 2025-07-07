using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Test_Azienda1.Utilities.Attributes
{
    public class AttributePassword : ValidationAttribute
    {
        private const string Pattern_Password = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=])(?=\S+$).{8,20}$";


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) // L'obbligatorietà è gestita con [Required]
                return ValidationResult.Success;
            if (!(value is string password))
                return new ValidationResult("La password è obbligatoria.");
            if (!Regex.IsMatch(password, Pattern_Password))
                return new ValidationResult("La password deve avere tra gli 8 e i 20 valori e deve comprendere lettere grandi, piccole, numeri e caratteri speciali.");
            return ValidationResult.Success;
        }
    }
}
