using MindMission.Domain.Constants;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.CustomValidation.DataAnnotation
{

    /// <summary>
    /// Validation attribute that checks if a list has a certain number of items.
    /// </summary>
    public class ListCountAttribute : ValidationAttribute
    {
        private readonly int _requiredCount;

        public ListCountAttribute(int requiredCount)
        {
            _requiredCount = requiredCount;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is ICollection collection && collection.Count < _requiredCount)
            {
                ErrorMessage = string.Format(ErrorMessages.IncorrectListCount, _requiredCount);
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}
