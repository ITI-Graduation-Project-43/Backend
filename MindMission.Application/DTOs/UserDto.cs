using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class UserDto
    {
        [Required(ErrorMessage = "The FirstName is required")]
        [MinLength(3, ErrorMessage = "FirstName Should be 3 characters at least")]
        [MaxLength(10, ErrorMessage = "FirstName Should be 10 characters at Most")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The LastName is required")]
        [MinLength(3, ErrorMessage = "LastName Should be 3 characters at least")]
        [MaxLength(10, ErrorMessage = "LastName Should be 10 characters at Most")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The password is required")]
        [MinLength(8, ErrorMessage = "Password Should be 8 characters at least")]
        public string Password { get; set; } = string.Empty;
    }
}