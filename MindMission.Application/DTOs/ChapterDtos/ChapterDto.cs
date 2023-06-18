using MindMission.Application.DTOs.Base;
using MindMission.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs.ChapterDtos
{
    public class ChapterDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int NoOfLessons { get; set; }
        public float NoOfHours { get; set; }
        public List<ChapterLessonDto> Lessons { get; set; } = new List<ChapterLessonDto>();
    }


    public class ChapterLessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LessonType Type { get; set; }
        public float NoOfHours { get; set; }
        public bool IsFree { get; set; }

    }
}
