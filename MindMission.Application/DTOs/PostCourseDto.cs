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

        [StringLength(2083)]
        public string CourseImage { get; set; } = string.Empty;

        [Required]
        public string InstructorId { get; set; } = string.Empty;
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
