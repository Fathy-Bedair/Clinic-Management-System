using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Clinic_Management_System.Validations
{
    public class ValidEgyptianPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {   
            if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string phoneNumber = value.ToString()!;
            string regexPattern = @"^(\+201|01|00201)[0125][0-9]{8}$";

            if (!Regex.IsMatch(phoneNumber, regexPattern))
            {
                return new ValidationResult(ErrorMessage ?? "The phone number is incorrect. It must consist of 11 digits and start with 010, 011, 012, or 015.");
            }
            return ValidationResult.Success;

        }
    }
}