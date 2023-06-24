using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Domain.Enums;

namespace MindMission.Application.DTOs.CourseChapters
{
    public class ChapterDto
    {
        public int Id { get; set; }

        [RequiredInteger("Course Id")]
        public int CourseId { get; set; }
        [RequiredField("Title")]
        [RangeStringLength(5, 100)]
        public string Title { get; set; } = string.Empty;
        [ListCount(1)]
        public List<LessonDto> Lessons { get; set; } = new();
    }
    public class LessonDto
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

        public AttachmentDto? Attachment { get; set; }
        public ArticleDto? Article { get; set; }
        public QuizDto? Quiz { get; set; }
        public VideoDto? Video { get; set; }
    }

    public class ArticleDto
    {
        public int Id { get; set; }
        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }
        [RequiredField("Content")]
        [MinStringLength(100)]
        public string Content { get; set; } = string.Empty;
    }



    public class VideoDto
    {
        public int Id { get; set; }
        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }
        [RequiredField("VideoUrl")]
        [MaxStringLength(2048)]
        public string VideoUrl { get; set; } = string.Empty;
    }

    public class AttachmentDto
    {
        public int Id { get; set; }

        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }

        public string FileName { get; set; } = string.Empty;

        [RequiredField("FileData")]
        public byte[]? FileData { get; set; }
        public bool IsDeleted { get; set; }
        public FileType FileType { get; set; }
    }
    public class QuizDto
    {
        public int Id { get; set; }

        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }

        [ListCount(1)]
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    }

    public class QuestionDto
    {
        public int Id { get; set; }
        [RequiredInteger("Quiz Id")]
        public int QuizId { get; set; }
        [RequiredField("Question Text")]
        [MaxStringLength(500)]
        public string QuestionText { get; set; } = string.Empty;

        [RequiredField("Choice A")]
        [MaxStringLength(255)]
        public string ChoiceA { get; set; } = string.Empty;

        [RequiredField("Choice B")]
        [MaxStringLength(255)]
        public string ChoiceB { get; set; } = string.Empty;

        [MaxStringLength(255)]
        public string ChoiceC { get; set; } = string.Empty;

        [MaxStringLength(255)]
        public string ChoiceD { get; set; } = string.Empty;
        [McqCorrectAnswer]
        public string CorrectAnswer { get; set; } = string.Empty;
    }

}
