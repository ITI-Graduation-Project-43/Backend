using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe.CustomValidationAttributes
{
    public class CardNumberValidator : ValidationAttribute
    {
        public static readonly string[] AvaialabeCardNumbers =
        {
            "4242424242424242",
            "4000056655665556",
            "5555555555554444",
            "2223003122003222",
            "5200828282828210",
            "5105105105105100"
        };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string CardNumber = value as string;

            if (CardNumber is null || CardNumber.Trim().Length == 0)
            {
                return new ValidationResult("Card Number is required");
            }

            if (Regex.IsMatch(CardNumber, @"^\d{16}$"))
            {
                if (AvaialabeCardNumbers.Contains(CardNumber))
                {
                    return ValidationResult.Success;
                };
                return new ValidationResult("Your card has been declined, try another one");

            }
            return new ValidationResult("Invalid Card Number");
        }
    }
}
