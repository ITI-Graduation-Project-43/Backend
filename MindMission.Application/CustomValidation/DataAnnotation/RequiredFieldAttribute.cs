using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a certain field is present.
    /// </summary>
    public class RequiredFieldAttribute : RequiredAttribute
    {

        public RequiredFieldAttribute(string fieldName)
        {
            ErrorMessage = string.Format(ErrorMessages.Required, fieldName);
        }


    }
}
