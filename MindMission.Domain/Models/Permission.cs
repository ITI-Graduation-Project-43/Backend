using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a permission entity that defines a specific action or operation that can be granted to administrators.
    /// </summary>
    public partial class Permission : BaseEntity, IEntity<int>, ISoftDeletable
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; } = string.Empty;

        [StringLength(2048)]
        [Unicode(false)]
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;


        [InverseProperty(nameof(AdminPermission.Permission))]
        public virtual ICollection<AdminPermission>? AdminPermissions { get; set; }
    }
}