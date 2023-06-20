using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a string follows the email format.
    /// </summary>
    public class CustomEmailAddressAttribute : ValidationAttribute
    {
        public CustomEmailAddressAttribute()
        {
            ErrorMessage = ErrorMessages.InvalidEmailFormat;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string email && !IsValidEmail(email))
            {
                return new ValidationResult(ErrorMessage);

            }

            return ValidationResult.Success!;
        }

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
