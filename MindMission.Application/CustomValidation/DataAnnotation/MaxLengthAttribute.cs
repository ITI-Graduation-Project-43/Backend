using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a string is under a certain length.
    /// </summary>
    public class MaxStringLengthAttribute : ValidationAttribute
    {
        private readonly int _maxLength;

        public MaxStringLengthAttribute(int maxLength)
        {
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string str && str.Length > _maxLength)
            {
                var errorMessage = string.Format(ErrorMessages.LengthAboveMaximum, _maxLength);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success!;
        }
    }

}
