using MindMission.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class EnrollmentDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        [Required]
        public int CourseId { get; set; }

        public string CourseTitle { get; set; } = string.Empty;
        public decimal CoursePrice { get; set; } = 0;
        public string CourseDescription { get; set; } = string.Empty;
        public string CourseImageUrl { get; set; } = string.Empty;
        [Required]

        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;

        public decimal? CourseAvgReview { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string InstructorId { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
        public string? InstructorProfilePicture { get; set; }
        public int CourseNoOfEnrollment { get; set; }
    }
}