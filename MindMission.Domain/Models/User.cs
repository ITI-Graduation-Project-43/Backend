using Microsoft.AspNetCore.Identity;
using MindMission.Domain.Common;

namespace MindMission.Domain.Models
{
    public partial class User : IdentityUser, IEntity<string>
    {
        public bool? IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeactivated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}