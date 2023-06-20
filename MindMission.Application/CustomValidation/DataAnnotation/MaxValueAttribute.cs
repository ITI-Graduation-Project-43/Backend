using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a value is below a certain maximum.
    /// </summary>
    public class MaxValueAttribute : ValidationAttribute
    {
        private readonly double _maxValue;

        public MaxValueAttribute(double maxValue)
        {
            _maxValue = maxValue;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IComparable comparable && comparable.CompareTo(_maxValue) > 0)
            {
                ErrorMessage = string.Format(ErrorMessages.ValueAboveMaximum, _maxValue);
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}
