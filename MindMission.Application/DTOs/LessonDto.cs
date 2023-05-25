using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class LessonDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int NoOfHours { get; set; }
        public bool IsFree { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /*  public Chapter Chapter { get; set; }
            public ICollection<String> Articles { get; set; }
            public ICollection<String> Attachments { get; set; }
            public ICollection<String> Discussions { get; set; }
            public ICollection<String> Quizzes { get; set; }
            public ICollection<String> Videos { get; set; }
        */
    }
}
