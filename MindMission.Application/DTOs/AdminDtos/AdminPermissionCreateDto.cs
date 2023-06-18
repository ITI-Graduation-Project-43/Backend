using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs.AdminDtos
{
    public class AdminPermissionCreateDto : IDtoWithId<int>
    {
        public int Id { get; set; }

        [RequiredField("Admin Id")]
        public int AdminId { get; set; }
        [RequiredField("Permission Id")]
        public int PermissionId { get; set; }

        public DateTime GrantedAt { get; set; } = DateTime.Now;
    }
}
