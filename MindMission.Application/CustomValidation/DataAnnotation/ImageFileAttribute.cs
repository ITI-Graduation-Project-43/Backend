using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.CustomValidation.DataAnnotation
{
    /// <summary>
    /// Validation attribute that checks if a string is a valid image file name.
    /// </summary>
    public class ImageFileAttribute : RegularExpressionAttribute
    {
        public ImageFileAttribute() : base(@"^.+\.(png|jpg|webp|jepg|avif)$")
        {
            ErrorMessage = ErrorMessages.InvalidImageFileFormat;
        }
    }
}
