using System.ComponentModel.DataAnnotations;

namespace MindMission.Domain.Stripe.CustomValidationAttributes
{
    public class ExpirationYearValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string ExpYear = value as string;

            if (ExpYear is null || ExpYear.Trim().Length == 0)
            {
                return new ValidationResult("Expiration Year is required");
            }

            if (int.TryParse(ExpYear, out int Exp_Year) && ExpYear.Length == 2)
            {
                if ((Exp_Year + 2000) >= DateTime.Now.Year)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Expired Card");
            }

            return new ValidationResult("Invalid Expiration Year, Enter it like '23'");
        }
    }
}