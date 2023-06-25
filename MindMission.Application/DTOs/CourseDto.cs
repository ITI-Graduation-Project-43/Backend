using MindMission.Application.DTOs.Base;
using MindMission.Domain.Enums;


namespace MindMission.Application.DTOs
{
    public class CourseDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Language Language { get; set; }
        public decimal Price { get; set; }
        public Level Level { get; set; }
        public decimal AvgReview { get; set; }
        public int NoOfReviews { get; set; }
        public int NoOfStudents { get; set; }
        public decimal Discount { get; set; }
        public int ChapterCount { get; set; }
        public int LessonCount { get; set; }
        public int NoOfVideos { get; set; }
        public int NoOfArticles { get; set; }
        public int NoOfAttachments { get; set; }
        public int NoOfQuizes { get; set; }

        public float NoOfHours { get; set; }
        public bool Published { get; set; }
        public bool Approved { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; } = string.Empty;
        public int TopicId { get; set; }

        public string TopicName { get; set; } = string.Empty;
        public string InstructorId { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
        public string InstructorBio { get; set; } = string.Empty;
        public string InstructorProfilePicture { get; set; } = string.Empty;
        public string InstructorTitle { get; set; } = string.Empty;
        public string InstructorDescription { get; set; } = string.Empty;
        public int InstructorNoOfCourses { get; set; }
        public int InstructorNoOfStudents { get; set; }
        public double? InstructorAvgRating { get; set; }
        public int InstructorNoOfRatings { get; set; }
        public ICollection<CourseChapterDto> Chapters { get; set; } = new List<CourseChapterDto>();

        public IList<LearningItemDto> LearningItems { get; set; } = null!;
        public IList<EnrollmentItemDto> EnrollmentItems { get; set; } = null!;
        public IList<CourseRequirementDto> CourseRequirements { get; set; } = null!;


    }
    public class LearningItemDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class EnrollmentItemDto
    {
        public string Description { get; set; } = string.Empty;
    }

    public class CourseRequirementDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }


    public class CourseChapterDto
    {
        public string Title { get; set; } = string.Empty;
        public int NoOfLessons { get; set; }
        public float NoOfHours { get; set; }
    }
}