using Microsoft.AspNetCore.Http;
using MindMission.Application.Custom_Validation;
using MindMission.Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.DTOs
{
    public class CourseCreateDto
    {

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        [StringLength(2048)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(CategoryType))]
        public CategoryType Category { get; set; }

        [Required]
        [EnumDataType(typeof(Language))]
        public Language Language { get; set; }

        [Required]
        [Range(0, 5000)]
        public decimal Price { get; set; }

        [Required]
        [EnumDataType(typeof(Level))]
        public Level Level { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [MaxFileSize(10_000_000)] // 10 MB
        [AllowedExtensions(new string[] { ".jpg", ".png", ".webp", ".jpeg", ".avif" })]
        public IFormFile CourseImage { get; set; } = new FormFile(null, 0, 0, null, null);
        [Required]
        public List<LearningItemCreateDto> LearningItems { get; set; } = new List<LearningItemCreateDto>();

        [Required]
        public List<EnrollmentItemCreateDto> EnrollmentItems { get; set; } = new List<EnrollmentItemCreateDto>();

        public List<CourseRequirementCreateDto> CourseRequirements { get; set; } = new List<CourseRequirementCreateDto>();
    }

    public class LearningItemCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }

    public class EnrollmentItemCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
    }

    public class CourseRequirementCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

}
