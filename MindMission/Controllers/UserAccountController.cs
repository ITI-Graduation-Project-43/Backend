using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.UserAccount;
using MindMission.Application.DTOs.UserDtos;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Services;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Repositories;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : BaseController<UserAccount, UserAccountDto, int>
    {
        private readonly IUserAccountService _context;
        private readonly UserAccountMappingService _UserAccountMappingService;
        private readonly UserAccountService _userAccountService;

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

        [HttpGet("UserAccount/{UserId}")]
        public async Task<ActionResult<IEnumerable<UserAccountDto>>> GetAccountsByUserId(string UserId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _context.GetAllByUserIdAsync(UserId, pagination.PageNumber, pagination.PageSize), _context.GetTotalCountAsync, pagination, "UserAccounts");          
        }

        [HttpPatch("Accounts")]
        public async Task<IActionResult> UpdateUserAccount(UserAccountsDto UserAccountsDto)
        {
            List<UserAccount> userAccounts = new List<UserAccount>();

            foreach (var account in UserAccountsDto.userAccounts)
            {
                userAccounts.Add(new UserAccount()
                {
                    Id = account.Id,
                    AccountId = account.AccountId,
                    UserId = UserAccountsDto.UserId,
                    AccountLink = account.accountDomain
                });
            }

            var UpdatedUserAccounts = await _context.UpdateUserAccount(userAccounts);
            foreach (var account in UpdatedUserAccounts)
            {
                foreach(var current in UserAccountsDto.userAccounts)
                {
                    if(current.AccountId == account.AccountId)
                    {
                        current.Id = account.Id;
                        break;
                    }
                }
            }

            return Ok(ResponseObjectFactory.CreateResponseObject(true, "The accounts is updated successfully", UserAccountsDto.userAccounts));
        }
    }
}