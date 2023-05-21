using MindMission.Domain.Enums;


namespace MindMission.Application.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
        public bool Approved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;
        public int? ParentCategoryId { get; set; } = null;
        public int? ParentSubCategoryId { get; set; } = null;
        public string? ParentCategoryName { get; set; } = null;
        public string? ParentSubCategoryName { get; set; } = null;
    }


}
