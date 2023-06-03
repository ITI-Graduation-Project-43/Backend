using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class InstructorDto : IDtoWithId<string>
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NoOfCources { get; set; }
        public int NoOfStudents { get; set; }
        public int NoOfCourses { get; set; }
        public double AvgRating { get; set; }
        public int NoOfRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Dictionary<string, string> accounts { get; set; } = new Dictionary<string, string>();
        public List<Dictionary<string, string>> Courses { get; set; } = new List<Dictionary<string, string>>();


    }
}