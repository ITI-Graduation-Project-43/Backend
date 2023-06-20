using System.ComponentModel.DataAnnotations;

namespace MindMission.Domain.DTOs
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Required*")]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required*")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required*")]
        [RegularExpression(@"^(?=.*[!@#$%^&*()])(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$", ErrorMessage = "Password must contain upper, lower characters, numbers and special characters")]

        [MinLength(8, ErrorMessage = "Must be 8 characters at least")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}