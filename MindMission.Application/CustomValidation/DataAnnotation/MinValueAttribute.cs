using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.CustomValidation.DataAnnotation
{

    /// <summary>
    /// Validation attribute that checks if a value is above a certain minimum.
    /// </summary>
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly double _minValue;

        public MinValueAttribute(double minValue)
        {
            _minValue = minValue;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IComparable comparable && comparable.CompareTo(_minValue) < 0)
            {
                ErrorMessage = string.Format(ErrorMessages.ValueBelowMinimum, _minValue);
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}
