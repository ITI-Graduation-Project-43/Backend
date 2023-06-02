using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;
    }
}