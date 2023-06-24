using MindMission.Application.DTOs;
using MindMission.Application.DTOs.Account;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class AccountMappingService : IMappingService<Account, AccountDto>
    {

        public AccountMappingService()
        {
        }

        public async Task<AccountDto> MapEntityToDto(Account account)
        {
            return new AccountDto()
            {
                Id = account.Id,
                AccountName = account.AccountName,
                AccountDomain = account.AccountDomain
            };
        }

        public Account MapDtoToEntity(AccountDto dto)
        {
            return new Account()
            {
                Id = dto.Id,
                AccountName = dto.AccountName,
                AccountDomain = dto.AccountDomain,
                IsDeleted = false
            };         
        }
    }
}
