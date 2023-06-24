using MindMission.Domain.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a list has a certain range number of items.
    /// </summary>
    public class RangeListCountAttribute : ValidationAttribute
    {
        private readonly int _minCount;
        private readonly int _maxCount;

        public RangeListCountAttribute(int minNum, int maxNum)
        {
            _minCount = minNum;
            _maxCount = maxNum;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is ICollection collection && (collection.Count < _minCount || collection.Count > _maxCount))
            {
                ErrorMessage = string.Format(ErrorMessages.IncorrectRangeListCount, _minCount, _maxCount);
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}
