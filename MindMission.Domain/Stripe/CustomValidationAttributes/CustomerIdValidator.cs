using System.ComponentModel.DataAnnotations;

namespace MindMission.Domain.Stripe.CustomValidationAttributes
{
    internal class CustomerIdValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string CustomerId = value as string;

            if (CustomerId != null && CustomerId.Trim().Length > 0)
            {
                if (CustomerId == ReturnedCutomerId.CustomerId)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Invalid Customer Id");
            }
            return new ValidationResult("Customer Id is required");
        }
    }

    public static class ReturnedCutomerId
    {
        public static string CustomerId = string.Empty;
    }
}