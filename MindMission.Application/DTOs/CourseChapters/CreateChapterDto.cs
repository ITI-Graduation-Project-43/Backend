using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.DTOs.AttachmentDtos;
using MindMission.Application.DTOs.QuizDtos;
using MindMission.Application.DTOs.VideoDtos;
using MindMission.Domain.Enums;

namespace MindMission.Application.DTOs.CourseChapters
{
    public class CreateChapterDto
    {
        public int Id { get; set; }

        [RequiredInteger("Course Id")]
        public int CourseId { get; set; }
        [RequiredField("Title")]
        [RangeStringLength(5, 100)]
        public string Title { get; set; } = string.Empty;
        [ListCount(1)]
        public List<CreateLessonDto> Lessons { get; set; } = new();
    }
    public class CreateLessonDto
    {
        public int Id { get; set; }

        [RequiredInteger("Chapter Id")]
        public int ChapterId { get; set; }

        [RequiredField("Title")]
        [RangeStringLength(5, 100)]
        public string Title { get; set; } = string.Empty;

        [RequiredField("Description")]
        [RangeStringLength(10, 500)]
        public string Description { get; set; } = string.Empty;

        public LessonType Type { get; set; }
        public float NoOfHours { get; set; }
        public AttachmentCreateDto? Attachment { get; set; }
        public ArticleCreateDto? Article { get; set; }
        public QuizCreateDto? Quiz { get; set; }
        public VideoCreateDto? Video { get; set; }
    }

}
