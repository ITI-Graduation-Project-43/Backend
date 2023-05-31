using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class AdminDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public bool IsDeactivated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
    }
}