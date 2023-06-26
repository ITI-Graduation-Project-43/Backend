using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    public partial class Admin : BaseEntity, IEntity<string>, ISoftDeletable
    {

        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; } = string.Empty;


        [StringLength(2048)]
        [Unicode(false)]
        public string ProfilePicture { get; set; } = string.Empty;
        public bool IsDeactivated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(Id))]
        public virtual User User { get; set; } = null!;

        [InverseProperty(nameof(AdminPermission.Admin))]
        public virtual ICollection<AdminPermission>? AdminPermissions { get; set; }
    }
}