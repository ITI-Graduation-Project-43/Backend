using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    public partial class Category : BaseEntity, IEntity<int>, ISoftDeletable
    {
        public Category()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public CategoryType Type { get; set; }

        public int? ParentId { get; set; }
        public bool Approved { get; set; }
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(InverseParent))]
        public virtual Category Parent { get; set; }

        [InverseProperty(nameof(Course.Category))]
        public virtual ICollection<Course> Courses { get; set; }

        [InverseProperty(nameof(Parent))]
        public virtual ICollection<Category> InverseParent { get; set; }
    }
}