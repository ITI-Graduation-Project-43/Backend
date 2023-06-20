using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs.PostDtos
{

    public class PostChapterDto
    {
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]

        public string Title { get; set; } = string.Empty;
    }
}
