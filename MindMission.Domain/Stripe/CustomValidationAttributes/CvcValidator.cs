﻿using System.ComponentModel.DataAnnotations;

namespace MindMission.Domain.Stripe.CustomValidationAttributes
{
    public class CvcValidator : ValidationAttribute
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