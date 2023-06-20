using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.CustomValidation.DataAnnotation
{

    /// <summary>
    /// Validation attribute that checks if a string length falls within a certain range.
    /// </summary>
    public class RangeStringLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public RangeStringLengthAttribute(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string str && (str.Length < _minLength || str.Length > _maxLength))
            {
                ErrorMessage = string.Format(ErrorMessages.LengthOutOfRange, _minLength, _maxLength);
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}
