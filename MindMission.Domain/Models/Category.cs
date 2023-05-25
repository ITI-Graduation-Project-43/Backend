using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    public partial class Category : IEntity<int>
    {
        public Category()
        {
            Courses = new HashSet<Course>();
            InverseParent = new HashSet<Category>();
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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(InverseParent))]
        public virtual Category Parent { get; set; }
        [InverseProperty(nameof(Course.Category))]
        public virtual ICollection<Course> Courses { get; set; }
        [InverseProperty(nameof(Parent))]
        public virtual ICollection<Category> InverseParent { get; set; }
    }
}
