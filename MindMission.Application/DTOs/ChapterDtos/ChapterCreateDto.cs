using System.ComponentModel.DataAnnotations;
using MindMission.Application.CustomValidation.DataAnnotation;

namespace MindMission.Application.DTOs.ChapterDtos
{
    internal class ChapterCreateDto
    {
        public int Id { get; set; }
        [RequiredField("Course Id")]
        public int CourseId { get; set; }
        [RangeValueAttribute(2, 100)]
        public string Title { get; set; } = string.Empty;

    }


}
