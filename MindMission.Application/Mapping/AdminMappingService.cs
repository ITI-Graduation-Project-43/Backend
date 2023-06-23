using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class AdminMappingService : IMappingService<Admin, AdminDto>
    {

        public AdminMappingService()
        {
        }

        public async Task<AdminDto> MapEntityToDto(Admin admin)
        {
            var permissions = admin.AdminPermissions.Select(ap => ap.PermissionId).ToList();


            return new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                ProfilePicture = admin.ProfilePicture,
                AdminPermissions = permissions
            };
        }

        public Admin MapDtoToEntity(AdminDto dto)
        {
            return new Admin
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ProfilePicture = dto.ProfilePicture,
            };
        }
    }
}