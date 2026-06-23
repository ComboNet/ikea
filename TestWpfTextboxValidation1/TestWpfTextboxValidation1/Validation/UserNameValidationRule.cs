using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestWpfTextboxValidation1.Validation
{
    public class UserNameValidationRule
    {

    }
    /*public class UserNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex("^[A_Za-z0-9!@#$%^&]{2,25}$");
            string? input = value.ToString();
            if (!regex.IsMatch(input))
            {
                if (input.Length < 2 || input.Length > 25)
                    return new ValidationResult(false, "username have at least 2 character and maximum 25 characters.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }*/
}
