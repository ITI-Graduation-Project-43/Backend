using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class TimeTrackingDto : IDtoWithId<Guid>
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int? EndHour { get; set; }
        public int? EndMinute { get; set; }
        public int? TimeSpend { get; set; }
    }
}
