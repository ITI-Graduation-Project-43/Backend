using MindMission.Application.DTOs.Base;
using MindMission.Domain.Enums;

namespace MindMission.Application.DTOs
{
    public class CategoryDto : IDtoWithId<int>, IEquatable<CategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CategoryType Type { get; set; } = CategoryType.Category;
        public bool Approved { get; set; } = false;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int? ParentCategoryId { get; set; } = null;
        public int? ParentSubCategoryId { get; set; } = null;
        public string? ParentCategoryName { get; set; } = null;
        public string? ParentSubCategoryName { get; set; } = null;

        public bool Equals(CategoryDto? other)
        {
            if (other == null)
                return false;

            return Id == other.Id &&
                   Name == other.Name &&
                   Type == other.Type &&
                   Approved == other.Approved &&
                   ParentCategoryId == other.ParentCategoryId &&
                   ParentSubCategoryId == other.ParentSubCategoryId;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CategoryDto);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}