using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.DTOs.PostDtos.Base
{
    public abstract class LessonDtoBase
    {
        public int LessonId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(2048, ErrorMessage = "Description cannot exceed 2048 characters.")]
        public string Description { get; set; } = string.Empty;

        [Range(0, float.MaxValue, ErrorMessage = "NoOfHours must be a positive float.")]
        public float NoOfHours { get; set; }

        public bool IsFree { get; set; } = true;
    }
}
