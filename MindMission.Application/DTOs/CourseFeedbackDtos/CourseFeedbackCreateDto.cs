namespace MindMission.Application.DTOs.CourseFeedbackDtos
{
    public class CourseFeedbackCreateDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string InstructorId { get; set; } = string.Empty;
        public decimal InstructorRating { get; set; }
        public decimal CourseRating { get; set; }
        public string FeedbackText { get; set; } = string.Empty;
    }
}
