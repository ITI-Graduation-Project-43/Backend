using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    public partial class User : IdentityUser, IEntity<string>
    {
        public override string Email { get => base.Email; set => base.Email = value; }
        public bool? IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeactivated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}