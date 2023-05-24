using Microsoft.AspNetCore.Http;
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
    public class UserAccountController : BaseController<UserAccount, UserAccountDto>
    {
        private readonly IUserAccountService _context;
        private readonly UserAccountMappingService _UserAccountMappingService;
        private readonly IUserService _userService;
        public UserAccountController(IUserAccountService context, IUserService userService, UserAccountMappingService userAccountMappingService ) : base(userAccountMappingService)
        {
            _context = context;
            _userService = userService;
            _UserAccountMappingService= userAccountMappingService;
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
