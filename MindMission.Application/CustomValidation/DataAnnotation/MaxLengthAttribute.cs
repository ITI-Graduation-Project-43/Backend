using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.CustomValidation.DataAnnotation
{
    public class MaxLengthAttribute : ValidationAttribute
    {
        private readonly int _maxLength;

        public MaxLengthAttribute(int maxLength)
        {
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string content && content.Length > _maxLength)
            {
                var errorMessage = string.Format(ErrorMessages.LengthExceeded, _maxLength);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
