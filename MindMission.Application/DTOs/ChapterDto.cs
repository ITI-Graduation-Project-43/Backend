using MindMission.Application.DTOs.Base;
using System.Text.Json.Serialization;

namespace MindMission.Application.DTOs
{
    public class ChapterDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int NoOfLessons { get; set; }
        public int NoOfHours { get; set; }
        public List<Dictionary<String, String>> Lessons { get; set; } = new List<Dictionary<String, String>>();
    }
}