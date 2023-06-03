using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    public partial class Admin : BaseEntity, IEntity<int>
    {
        public Admin()
        {
            AdminPermissions = new HashSet<AdminPermission>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        [Unicode(false)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(2048)]
        [Unicode(false)]
        public string ProfilePicture { get; set; }

        [Required]
        [StringLength(2048)]
        public string PasswordHash { get; set; }

        public bool IsDeactivated { get; set; }

        [InverseProperty(nameof(AdminPermission.Admin))]
        public virtual ICollection<AdminPermission> AdminPermissions { get; set; }
    }
}