using Microsoft.AspNetCore.Identity;
using MindMission.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class UserDto
    {
        [Required(ErrorMessage ="The email is required")]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The email is required")]
        [MinLength(8, ErrorMessage = "Password Should be 8 characters at least")]
        public string Password { get; set; } = string.Empty;
    }
}
