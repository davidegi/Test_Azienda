using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Test_Azienda1.Utilities.Attributes
{
    public class AttributeCAP : ValidationAttribute
    {
        private const string Pattern_CAP = @"^\d{5}$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) // L'obbligatorietà è gestita con [Required]
                return ValidationResult.Success;
            if (value is not string cap)
                return new ValidationResult("Il CAP è obbligatorio.");
            if (!Regex.IsMatch(cap, Pattern_CAP))
                return new ValidationResult("Il CAP deve essere composto da 5 cifre numeriche.");
            return ValidationResult.Success;
        }
    }
}