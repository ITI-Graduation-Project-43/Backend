using MindMission.Application.DTOs.Account;
using MindMission.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class StudentDto : IDtoWithId<string>
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; } = string.Empty;
        public int NoOfCourses { get; set; }
        public int NoOfWishlist { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<AccountDto> Accounts { get; set; } = new List<AccountDto>();


    }
}