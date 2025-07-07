using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Test_Azienda1.Utilities.Attributes
{
    public class AttributeCF : ValidationAttribute
    {
        private const string Pattern_CodiceFiscale = @"^(?:[a-zA-Z][aeiouAEIOU][aeiouxAEIOUX]|[b-dB-Df-hF-Hj-nJ-Np-tP-Tv-zV-Z]{2}[a-zA-Z]){2}(?:[\dlmnLMNp-vP-V]{2}(?:[a-eA-EhHlLmMpPr-tR-T](?:[04lLqQ][1-9mMnNp-vP-V]|[15mMrR][\dlLmMnNp-vP-V]|[26nNsS][0-8lLmMnNp-uP-U])|[dDhHpPsS][37pPtT][0lL]|[aAcCeElLmMrRtT][37pPtT][01lLmM]|[aAc-eC-EhHlLmMpPr-tR-T][26nNsS][9vV])|(?:[02468lLnNqQsSuU][048lLqQuU]|[13579mMpPrRtTvV][26nNsS])[bB][26nNsS][9vV])(?:[a-mA-MzZ][1-9mMnNp-vP-V][\dlLmMnNp-vP-V]{2}|[a-mA-M][0lL](?:[1-9mMnNp-vP-V][\dlLmMnNp-vP-V]|[0lL][1-9mMnNp-vP-V]))[a-zA-Z]$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) // L'obbligatorietà è gestita con [Required]
                return ValidationResult.Success;
            if (!(value is string codiceFiscale))
                return new ValidationResult("Il codice fiscale è obbligatorio.");
            if (!Regex.IsMatch(codiceFiscale, Pattern_CodiceFiscale))
                return new ValidationResult("Il codice fiscale deve essere composto da 16 valori.");
            return ValidationResult.Success;
        }
    }
}