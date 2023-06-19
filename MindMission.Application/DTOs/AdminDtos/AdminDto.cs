using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs.AdminDtos
{
    public class AdminDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => FirstName + " " + LastName;
        public string Email { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public bool IsDeactivated { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<AdminPermissionDto> AdminPermissions { get; set; } = new List<AdminPermissionDto>();
    }
}
