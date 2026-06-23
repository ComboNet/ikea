using System.Globalization;
using System.Windows.Controls;

namespace TestWpfTextboxValidation1.Validation
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value as string, out _))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "Invalid number."); ;
        }
    }
}
