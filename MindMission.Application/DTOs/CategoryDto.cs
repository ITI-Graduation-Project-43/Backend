using MindMission.Application.DTOs.Base;
using MindMission.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class CategoryDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]

        public CategoryType Type { get; set; } = CategoryType.Category;
        public bool Approved { get; set; } = false;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int? ParentCategoryId { get; set; } = null;
        public int? ParentSubCategoryId { get; set; } = null;
        public string? ParentCategoryName { get; set; } = null;
        public string? ParentSubCategoryName { get; set; } = null;

    }
}