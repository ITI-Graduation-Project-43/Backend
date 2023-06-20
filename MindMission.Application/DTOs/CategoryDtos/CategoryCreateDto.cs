using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Domain.Enums;


namespace MindMission.Application.DTOs.CategoryDtos
{
    internal class CategoryCreateDto
    {
        [RequiredField("Id")]
        public int Id { get; set; }
        [RequiredField("Name")]
        [RangeValueAttribute(2, 100)]
        public string Name { get; set; } = string.Empty;
        [RequiredField("Type")]

        public CategoryType Type { get; set; } = CategoryType.Category;
        public int? ParentId { get; set; } = null;
    }

}
