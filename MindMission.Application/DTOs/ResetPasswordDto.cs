using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.DTOs
{
    public class ResetPasswordDto
    {
        public string Token { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required*")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required*")]
        [Compare(nameof(Password), ErrorMessage ="Not matched")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; } = string.Empty;
    }
}
