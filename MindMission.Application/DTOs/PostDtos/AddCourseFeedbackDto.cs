using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.DTOs.PostDtos
{
    public class AddCourseFeedbackDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required*")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Required*")]
        public string StudentId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Required*")]
        public string InstructorId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required*"), Range(0.0, 5.0, ErrorMessage = "The rate must be between 0 to 5")]
        public decimal InstructorRating { get; set; }

        [Required(ErrorMessage = "Required*"), Range(0.0, 5.0, ErrorMessage = "The rate must be between 0 to 5")]
        public decimal CourseRating { get; set; }

        [Required(ErrorMessage = "Required*"), StringLength(2048, ErrorMessage = "The maximum character os 2048")]
        public string FeedbackText { get; set; } = string.Empty;
    }
}
