using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a string only contains alphabetic characters.
    /// </summary>
    public class AlphabeticAttribute : RegularExpressionAttribute
    {
        public AlphabeticAttribute() : base(@"^[A-Za-z\s]+$")
        {
            ErrorMessage = ErrorMessages.InvalidAlphabeticCharacters;
        }
    }
}
