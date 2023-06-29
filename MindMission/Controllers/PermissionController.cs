using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController<Permission, PermissionDto, int>
    {
        private readonly IPermissionService _permissionService;
        private readonly PermissionMappingService _permissionMappingService;

        public PermissionController(IPermissionService permissionService, PermissionMappingService permissionMappingService) :
            base(permissionMappingService)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _permissionMappingService = permissionMappingService ?? throw new ArgumentNullException(nameof(permissionMappingService));
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<PermissionDto>>> GetAllPermissions([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(()=>_permissionService.GetAllAsync(pagination.PageNumber, pagination.PageSize), _permissionService.GetTotalCountAsync, pagination, "Permission");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDto>> GetPermissionById(int id)
        {
            return await GetEntityResponse(() => _permissionService.GetByIdAsync(id, p => p.AdminPermissions), "Permission");
        }

        [HttpPost("Permission")]
        public async Task<ActionResult<PermissionDto>> AddPermission([FromBody] PermissionDto permissionDto)
        {
            return await AddEntityResponse(_permissionService.AddAsync, permissionDto, "Permission", nameof(GetPermissionById));
        }

        [HttpPut("{permissionId}")]
        public async Task<ActionResult> UpdatePermission(int permissionId, PermissionDto permissionDto)
        {
            return await UpdateEntityResponse(_permissionService.GetByIdAsync, _permissionService.UpdateAsync, permissionId, permissionDto, "Permission");
        }

        [HttpDelete("Delete/{permissionId}")]
        public async Task<IActionResult> DeletePermission(int permissionId)
        {
            return await DeleteEntityResponse(_permissionService.GetByIdAsync, _permissionService.DeleteAsync, permissionId);
        }

        // DELETE: api/Permission/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var course = await _permissionService.GetByIdAsync(id);

            if (course == null)
                return NotFound(NotFoundResponse("Course"));
            await _permissionService.SoftDeleteAsync(id);
            return NoContent();
        }


    }
}