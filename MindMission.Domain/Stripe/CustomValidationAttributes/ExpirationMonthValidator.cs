using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe.CustomValidationAttributes
{
    public class ExpirationMonthValidator : ValidationAttribute
    {
        private readonly string _ExpYear;

        public ExpirationMonthValidator(string ExpYear)
        {
            _ExpYear = ExpYear;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string ExpMonth = value as string;  

            if (ExpMonth is null || ExpMonth.Trim().Length == 0)
            {
                return new ValidationResult("Expiration Month is required");
            }

            if(int.TryParse(ExpMonth, out int Exp_Month) && Exp_Month > 0 && Exp_Month < 13)
            {
                var ExpYearProperty = validationContext.ObjectType.GetProperty(_ExpYear);
                if(ExpYearProperty != null)
                {
                    string ExpYear = ExpYearProperty.GetValue(validationContext.ObjectInstance) as string;
                    if (ExpYear != null)
                    {
                        if (int.TryParse(ExpYear, out int ExpYearInt))
                        {
                            if (ExpYearInt + 2000 == DateTime.Now.Year && Exp_Month < DateTime.Now.Month)
                            {
                                return new ValidationResult("Expired Card");
                            }
                        }
                    }
                }
                return ValidationResult.Success;                
            }
            return new ValidationResult("Invalid Expiration Month");
        }
    }
}
