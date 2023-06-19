using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks whether a integer value falls within a certain range.
    /// </summary>
    public class RequiredIntegerAttribute : RangeAttribute
    {
        public RequiredIntegerAttribute(string fieldName)
            : base(1, int.MaxValue)
        {
            ErrorMessage = string.Format(ErrorMessages.Required, fieldName);
        }
    }

}
