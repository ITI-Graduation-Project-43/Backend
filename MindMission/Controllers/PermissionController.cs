using Microsoft.AspNetCore.Mvc;
using MindMission.Application.Services;

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
            return Ok(await _permissionService.GetAllAsync());
        }
    }
}
