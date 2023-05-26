using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class LessonDto : IDtoWithId
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int NoOfHours { get; set; }
        public bool IsFree { get; set; }
    }
}
