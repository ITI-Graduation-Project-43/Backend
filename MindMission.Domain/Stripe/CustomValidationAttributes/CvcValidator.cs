using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe.CustomValidationAttributes
{
    internal class CvcValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string CVC = value as string;

            if (CVC == null || CVC.Trim().Length == 0)
            {
                return new ValidationResult("CVC is required");
            }


            if (int.TryParse(CVC, out int Cvc) && CVC.Length == 3)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid CVC");
        }
    }
}
