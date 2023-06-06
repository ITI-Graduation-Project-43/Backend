using MindMission.Application.DTOs.Base;
using MindMission.Domain.Enums;

namespace MindMission.Application.DTOs
{
    public class LessonDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LessonType Type { get; set; }
        public int NoOfHours { get; set; }
        public bool IsFree { get; set; }
        public int ChapterId { get; set; }
        public string ChapterTitle { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }


    }
}