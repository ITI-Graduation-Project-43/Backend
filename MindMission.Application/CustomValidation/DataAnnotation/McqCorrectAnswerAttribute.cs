using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a provided answer is a single character between A-D.
    /// </summary>
    public class McqCorrectAnswerAttribute : ValidationAttribute
    {
        public const int AnswerMaxLength = 1;
        public const string Pattern = "^[A-D]$";

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string answer)
            {
                if (string.IsNullOrWhiteSpace(answer))
                {
                    ErrorMessage = ErrorMessages.AnswerRequired;
                    return new ValidationResult(ErrorMessage);
                }
                if (answer.Length > AnswerMaxLength)
                {
                    ErrorMessage = ErrorMessages.AnswerTooLong;
                    return new ValidationResult(ErrorMessage);
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(answer, Pattern))
                {
                    ErrorMessage = ErrorMessages.InvalidAnswerFormat;
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
