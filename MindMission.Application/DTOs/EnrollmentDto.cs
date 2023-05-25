using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class EnrollmentDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public decimal CoursePrice { get; set; }
        public string CourseShortDescription { get; set; } = string.Empty;
        public string CourseImageUrl { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
    }
}
