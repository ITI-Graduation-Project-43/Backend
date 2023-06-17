using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents an admin entity.
    /// </summary>
    public partial class Admin : BaseEntity, IEntity<int>, ISoftDeletable
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public string FullName => FirstName + " " + LastName;

        [Required]
        [StringLength(255)]
        [Unicode(false)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(2048)]
        [Unicode(false)]
        public string ProfilePicture { get; set; } = string.Empty;

        [Required]
        [StringLength(2048)]
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsDeactivated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        [InverseProperty(nameof(AdminPermission.Admin))]
        public virtual ICollection<AdminPermission>? AdminPermissions { get; set; }
    }
}