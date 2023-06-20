using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a value falls within a certain range.
    /// </summary>
    public class RangeValueAttribute : ValidationAttribute
    {
        private readonly double _minValue;
        private readonly double _maxValue;

        public RangeValueAttribute(double minValue, double maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IComparable comparable && (comparable.CompareTo(_minValue) < 0 || comparable.CompareTo(_maxValue) > 0))
            {
                ErrorMessage = string.Format(ErrorMessages.RangeValueExceeded, _minValue, _maxValue);
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success!;
        }
    }
}
