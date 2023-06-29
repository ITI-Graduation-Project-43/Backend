using Microsoft.AspNetCore.Identity;
using MindMission.Domain.Common;

namespace MindMission.Domain.Models
{
    public partial class User : IdentityUser, IEntity<string>
    {
        public bool IsActive { get; set; } = true;
        public bool IsBlocked { get; set; } = false;
        public bool IsDeactivated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    }
}