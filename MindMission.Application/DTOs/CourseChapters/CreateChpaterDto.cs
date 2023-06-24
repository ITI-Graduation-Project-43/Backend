using Microsoft.AspNetCore.Http;
using MindMission.Application.CustomValidation.DataAnnotation;
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

        [RequiredField("Title")]
        [RangeStringLength(10, 500)]
        public string Description { get; set; } = string.Empty;

        public LessonType Type { get; set; }

        public CreateAttachmentDto? Attachment { get; set; }
        public CreateArticleDto? Article { get; set; }
        public CreateQuizDto? Quiz { get; set; }
        public CreateVideoDto? Video { get; set; }
    }

    public class CreateArticleDto
    {
        public int Id { get; set; }
        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }
        [RequiredField("Content")]
        [MinStringLength(100)]
        public string Content { get; set; } = string.Empty;
    }



    public class CreateVideoDto
    {
        public int Id { get; set; }
        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }
        [RequiredField("VideoUrl")]
        [MaxStringLength(2048)]
        public string VideoUrl { get; set; } = string.Empty;
    }

    public class CreateAttachmentDto
    {
        public int Id { get; set; }

        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }

        [RequiredField("VideoUrl")]
        [MaxStringLength(2048)]
        public string AttachmentUrl { get; set; } = string.Empty;
        [MaxStringLength(2048)]
        public string AttachmentName { get; set; } = string.Empty;
        [MaxStringLength(10)]
        public string AttachmentType { get; set; } = string.Empty;
        [MaxStringLength(50)]
        public string AttachmentSize { get; set; } = string.Empty;

    }
    public class CreateQuizDto
    {
        public int Id { get; set; }

        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }

        [ListCount(1)]
        public List<CreateQuestionDto> Questions { get; set; } = new List<CreateQuestionDto>();
    }

    public class CreateQuestionDto
    {
        public int Id { get; set; }
        [RequiredInteger("Quiz Id")]
        public int QuizId { get; set; }
        [RequiredField("Question Text")]
        [MaxStringLength(500)]
        public string QuestionText { get; set; } = string.Empty;
        [RangeListCount(2, 4)]
        public List<string> Choices { get; set; } = new List<string>();
        public string CorrectAnswer { get; set; } = string.Empty;
    }

}
