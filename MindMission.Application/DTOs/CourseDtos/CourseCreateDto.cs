using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.Base;
using MindMission.Domain.Enums;

namespace MindMission.Application.DTOs.CourseDtos
{
    public class CourseCreateDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        [RequiredField("Course Title")]
        [RangeValueAttribute(2, 100)]
        public string Title { get; set; } = string.Empty;
        [RequiredField("Short Description")]
        [RangeValueAttribute(2, 100)]
        public string ShortDescription { get; set; } = string.Empty;
        [RequiredField("Description")]
        [RangeValueAttribute(100, 10000)]
        public string Description { get; set; } = string.Empty;
        [ImageFileAttribute]
        public string ImageUrl { get; set; } = string.Empty;

        public Language Language { get; set; }
        [RangeValueAttribute(0, 5000)]

        public decimal Price { get; set; }
        public Level Level { get; set; }
        [RangeValueAttribute(0, 1)]

        public decimal? Discount { get; set; }
        [RequiredField("TopicId")]

        public int TopicId { get; set; }
        [RequiredField("Instructor Id")]

        public string InstructorId { get; set; } = string.Empty;
        public ICollection<LearningItemCreateDto> LearningItems { get; set; } = new List<LearningItemCreateDto>();
        public ICollection<EnrollmentItemCreateDto>? EnrollmentItems { get; set; } = new List<EnrollmentItemCreateDto>();
        public ICollection<CourseRequirementCreateDto>? CourseRequirements { get; set; }


    }
    public class LearningItemCreateDto
    {
        [RequiredField("Learning item title")]
        [RangeValueAttribute(2, 150)]
        public string Title { get; set; } = string.Empty;


        [RequiredField("Learning item description")]
        [RangeValueAttribute(10, 150)]
        public string Description { get; set; } = string.Empty;
    }

    public class EnrollmentItemCreateDto
    {
        [RequiredField("Enrollment item description")]
        [RangeValueAttribute(10, 150)]
        public string Description { get; set; } = string.Empty;
    }

    public class CourseRequirementCreateDto
    {
        [RequiredField("Requirement title")]
        [RangeValueAttribute(2, 150)]

        public string Title { get; set; } = string.Empty;
        [RequiredField("Requirement description")]
        [RangeValueAttribute(10, 150)]
        public string Description { get; set; } = string.Empty;
    }

}
