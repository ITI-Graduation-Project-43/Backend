using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.Services;
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
            var permission = await _permissionService.GetAllAsync();
            
            ResponseObject<Permission> AllPermission = new ResponseObject<Permission>();
            AllPermission.ReturnedResponse(true, "All permissions", permission, 3, 10, permission.Count());

            return Ok(AllPermission);
        }
    }
}
