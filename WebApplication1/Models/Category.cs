using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public partial class Category
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
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string Type { get; set; }
        public int? ParentId { get; set; }
        public bool Approved { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(Category.InverseParent))]
        public virtual Category Parent { get; set; }
        [InverseProperty(nameof(Course.Category))]
        public virtual ICollection<Course> Courses { get; set; }
        [InverseProperty(nameof(Category.Parent))]
        public virtual ICollection<Category> InverseParent { get; set; }
    }
}
