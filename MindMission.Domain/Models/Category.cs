using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{

    /// <summary>
    /// Represents a category entity that categorizes courses. It can be either a category, subcategory, or topic.
    /// Courses are associated with topics.
    /// </summary>
    public partial class Category : BaseEntity, IEntity<int>, ISoftDeletable
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public CategoryType Type { get; set; }

        public int? ParentId { get; set; }
        public bool Approved { get; set; } = false;
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(InverseParent))]
        public virtual Category Parent { get; set; } = null!;

        [InverseProperty(nameof(Course.Category))]
        public virtual ICollection<Course>? Courses { get; set; }

        [InverseProperty(nameof(Parent))]
        public virtual ICollection<Category>? InverseParent { get; set; }
    }
}