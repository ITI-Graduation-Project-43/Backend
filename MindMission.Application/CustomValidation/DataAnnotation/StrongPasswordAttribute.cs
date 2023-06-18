
using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a string fulfills certain password strength requirements.
    /// </summary>
    public class StrongPasswordAttribute : RegularExpressionAttribute
    {
        public StrongPasswordAttribute() : base(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
        {
            ErrorMessage = ErrorMessages.PasswordShouldBeStrong;
        }
    }

}
