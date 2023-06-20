using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a string is of a certain minimum length.
    /// </summary>
    public class MinStringLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public MinStringLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string str && str.Length < _minLength)
            {
                ErrorMessage = string.Format(ErrorMessages.LengthBelowMinimum, _minLength);
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }

}
