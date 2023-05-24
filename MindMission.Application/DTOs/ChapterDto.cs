using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class ChapterDto : IDtoWithId
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int NoOfLessons { get; set; }
        public int NoOfHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
