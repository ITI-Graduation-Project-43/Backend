using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController<Admin,AdminDto,int>
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService, AdminMappingService adminMappingService) : base(adminMappingService)
        {
            _adminService = adminService;
        }
        #region GET
        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IQueryable<AdminDto>>> GetAllAdmin([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(_adminService.GetAllAsync, pagination, "Admins");
        }

        // GET: api/Admin/{AdminId}
        [HttpGet("{AdminId}")]
        public async Task<ActionResult<AdminDto>> GetAdminById(int AdminId)
        {
            return await GetEntityResponse(() => _adminService.GetByIdAsync(AdminId), "Admin");
        }
        #endregion

        #region Add 

        // POST: api/Admin
        [HttpPost]
        public async Task<ActionResult<AdminDto>> AddAdmin([FromBody] AdminDto adminDTO)
        {
            return await AddEntityResponse(_adminService.AddAsync, adminDTO, "Admin", nameof(GetAdminById));
        }

        #endregion

        #region Delete
        // DELETE: api/Admin/{AdminId}
        [HttpDelete("{AdminId}")]
        public async Task<IActionResult> DeleteAdmin(int AdminId)
        {
            return await DeleteEntityResponse(_adminService.GetByIdAsync, _adminService.DeleteAsync, AdminId);
        }
        #endregion

        #region Edit Put 
        // PUT: api/Admin/{AdminId}
        [HttpPut("{AdminId}")]
        public async Task<ActionResult> UpdateAdmin(int AdminId, AdminDto adminDto)
        {
            return await UpdateEntityResponse(_adminService.GetByIdAsync, _adminService.UpdateAsync, AdminId, adminDto, "Admin");
        }
        #endregion

    }
}
