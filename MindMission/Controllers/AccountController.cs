using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.Account;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountDto, int>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService, AccountMappingService accountMappingService, ILogger<AccountController> logger) : base(accountMappingService)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<AccountDto>>> GetAllAccounts([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithInclude(_accountService.GetAllAsync, pagination, "Account");
        }
    }
}
