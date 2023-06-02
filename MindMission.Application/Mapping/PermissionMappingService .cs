using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Description = permission.Description
            };


            return permissionDto;
        }
    }

}
