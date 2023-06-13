using MindMission.Domain.Enums;


namespace MindMission.Application.DTOs
{

    public class CustomLessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LessonType Type { get; set; }
        public CustomArticleDto Article { get; set; }
        public CustomQuizDto Quiz { get; set; }
        public CustomVideoDto Video { get; set; }
    }
    public class CustomArticleDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }

    public class CustomQuizDto
    {
        public int Id { get; set; }
        public List<CustomQuestionDto> Questions { get; set; }
    }

    public class CustomVideoDto
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
    }

    public class CustomQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string ChoiceA { get; set; }
        public string ChoiceB { get; set; }
        public string ChoiceC { get; set; }
        public string ChoiceD { get; set; }
        public string CorrectAnswer { get; set; }
    }



}
