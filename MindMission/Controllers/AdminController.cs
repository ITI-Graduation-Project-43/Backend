using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : BaseController<Admin, AdminDto, int>
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IAdminService adminService, AdminMappingService adminMappingService, ILogger<AdminController> logger) : base(adminMappingService)
        {
            _adminService = adminService;
            _logger = logger;
        }




        #region GET

        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IQueryable<AdminDto>>> GetAllAdmin([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithInclude(
                _adminService.GetAllAsync,
                pagination,
                "Admins",
                admin => admin.AdminPermissions
            );
        }

        // GET: api/Admin/{Id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<AdminDto>> GetAdminById(int Id)
        {
            return await GetEntityResponseWithInclude(
                    () => _adminService.GetByIdAsync(Id,
                        admin => admin.AdminPermissions
                    ),
                    "Admin"
                );
        }

        #endregion GET

        #region Add

        // POST: api/Admin
        [HttpPost]
        public async Task<ActionResult<AdminDto>> AddAdmin([FromBody] AdminDto adminDTO)
        {
            return await AddEntityResponse(_adminService.AddAsync, adminDTO, "Admin", nameof(GetAdminById));
        }

        #endregion Add

        #region Delete

        // DELETE: api/Admin/{AdminId}
        [HttpDelete("{AdminId}")]
        public async Task<IActionResult> DeleteAdmin(int AdminId)
        {
            return await DeleteEntityResponse(_adminService.GetByIdAsync, _adminService.DeleteAsync, AdminId);
        }

        #endregion Delete

        #region Edit Put

        // PUT: api/Admin/{AdminId}
        [HttpPut("{AdminId}")]
        public async Task<ActionResult> UpdateAdmin(int AdminId, AdminDto adminDto)
        {
            return await UpdateEntityResponse(_adminService.GetByIdAsync, _adminService.UpdateAsync, AdminId, adminDto, "Admin");
        }

        // PATCH: api/Admin/{AdminId}
        [HttpPatch("{AdminId}")]
        public async Task<ActionResult> PartiallyUpdateAdmin(int AdminId, [FromBody] JsonPatchDocument<AdminDto> patchDoc)
        {

            return await PatchEntityResponse(_adminService.GetByIdAsync, _adminService.UpdateAsync, AdminId, "Admin", patchDoc);

        }

        #endregion Edit Put


    }
}