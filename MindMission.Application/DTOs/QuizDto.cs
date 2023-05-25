using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class QuizDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int NoOfQuestions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
