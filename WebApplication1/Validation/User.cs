using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    public partial class User : IdentityUser
    {
        [NotMapped]
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        public bool? IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeactivated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}