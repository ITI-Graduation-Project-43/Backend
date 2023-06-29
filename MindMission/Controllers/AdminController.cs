using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Services;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : BaseController<Admin, AdminDto, string>
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IAdminService adminService, AdminMappingService adminMappingService, ILogger<AdminController> logger) : base(adminMappingService)
        {
            _adminService = adminService;
            _logger = logger;
        }

        #region GET

        [HttpGet]
        public async Task<ActionResult<IQueryable<AdminDto>>> GetAllAdmin([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithIncludePagination(
                _adminService.GetAllAsync,
                _adminService.GetTotalCountAsync,
                pagination,
                "Admins",
                admin => admin.AdminPermissions
            );
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AdminDto>> GetAdminById(int Id)
        {
            //return await GetEntityResponseWithInclude(
            //        () => _adminService.GetByIdAsync(Id,
            //            admin => admin.AdminPermissions
            //        ),
            //        "Admin"
            //    );
            return Ok();
        }

        #endregion GET

        #region Edit Put
        [HttpPut("{AdminId}")]
        public async Task<ActionResult> UpdateAdmin(int AdminId, AdminDto adminDto)
        {
            //return await UpdateEntityResponse(_adminService.GetByIdAsync, _adminService.UpdateAsync, AdminId, adminDto, "Admin");
            return Ok();
        }

        [HttpPatch("{AdminId}")]
        public async Task<ActionResult> PartiallyUpdateAdmin(int AdminId, [FromBody] JsonPatchDocument<AdminDto> patchDoc)
        {

            //return await PatchEntityResponse(_adminService.GetByIdAsync, _adminService.UpdateAsync, AdminId, "Admin", patchDoc);
            return Ok();
        }

        #endregion Edit Put

    }
}