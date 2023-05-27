using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.DTOs
{
    public class ChangeEmailDto
    {
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string OldEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string NewEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required")]
        public string Password { get; set; } = string.Empty;
    }
}
