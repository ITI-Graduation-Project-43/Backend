using MindMission.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class StudentDto : IDtoWithId<string>
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage ="FirstName is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bio is required")]
        public string Bio { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; } = string.Empty;

        public int NumCourses { get; set; }
        public int NumWishlist { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Dictionary<string, string> accounts { get; set; } = new Dictionary<string, string>();


    }
}