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
    public class UserAccountController : BaseController<UserAccount, UserAccountDto, int>
    {
        private readonly IUserAccountService _context;
        private readonly UserAccountMappingService _UserAccountMappingService;

        public UserAccountController(IUserAccountService context, UserAccountMappingService userAccountMappingService) : base(userAccountMappingService)
        {
            _context = context;
            _UserAccountMappingService = userAccountMappingService;
        }

        [HttpPost]
        public async Task<ActionResult<UserAccount>> AddAccount([FromBody] UserAccountDto UserAccountDTO)
        {
            var newAccount = _UserAccountMappingService.MapDtoToEntity(UserAccountDTO);
            await _context.AddAsync(newAccount);
            return Ok(newAccount);
        }


    }
}