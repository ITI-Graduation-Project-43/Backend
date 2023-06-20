using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs.AdminDtos
{
    public class AdminPermissionDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public string PermissionName { get; set; } = string.Empty;
        public string PermissionDescription { get; set; } = string.Empty;
        public DateTime GrantedAt { get; set; }
    }
}
