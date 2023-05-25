using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var permissions = await _permissionService.GetAllAsync(permission => permission.AdminPermissions);
            List<PermissionDto> permissionDtos = new List<PermissionDto>();
            foreach (var item in permissions)
            {
                permissionDtos.Add(new PermissionDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    AdminIds = item.AdminPermissions.Select(ap => ap.Id).ToList()
                });
            }

            ResponseObject<PermissionDto> AllPermission = new();
            AllPermission.ReturnedResponse(true, "All permissions", permissionDtos, 3, 10, permissionDtos.Count());

            return Ok(AllPermission);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Permission permission = await _permissionService.GetByIdAsync(id, permission => permission.AdminPermissions);
            PermissionDto permissionDto = new PermissionDto()
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description,
                AdminIds = permission.AdminPermissions.Select(ap => ap.Id).ToList()
            };

            ResponseObject<PermissionDto> OnePermission = new();
            List<PermissionDto> permissions = new() { permissionDto };
            OnePermission.ReturnedResponse(true, "One permissions", permissions, 3, 10, permissions.Count());

            return Ok(permissionDto);
        }
    }
}
