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
                Email = admin.Email,
                ProfilePicture = admin.ProfilePicture,
                PasswordHash = admin.PasswordHash,
                IsDeactivated = admin.IsDeactivated,
                CreatedAt = admin.CreatedAt,
                UpdatedAt = admin.UpdatedAt,
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
                Email = dto.Email,
                ProfilePicture = dto.ProfilePicture,
                PasswordHash = dto.PasswordHash,
                IsDeactivated = dto.IsDeactivated,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }
    }
}