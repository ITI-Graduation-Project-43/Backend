using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class PermissionMappingService : IMappingService<Permission, PermissionDto>
    {
        public Permission MapDtoToEntity(PermissionDto permissionDto)
        {
            return new Permission
            {
                Id = permissionDto.Id,
                Name = permissionDto.Name,
                Description = permissionDto.Description
            };
        }

        public async Task<PermissionDto> MapEntityToDto(Permission permission)
        {
            var permissionDto = new PermissionDto
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description,
                AdminIds = permission.AdminPermissions.Select(x => x.Id).ToList()
            };
            return permissionDto;
        }
    }

}
