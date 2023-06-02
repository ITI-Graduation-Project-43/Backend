using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class UserAccountMappingService : IMappingService<UserAccount, UserAccountDto>
    {
        public UserAccount MapDtoToEntity(UserAccountDto dto)
        {
            return new UserAccount
            {
                UserId = dto.UserId,
                AccountId = dto.accountId,
                AccountLink = dto.accountLink,
            };
        }

        public async Task<UserAccountDto> MapEntityToDto(UserAccount entity)
        {
            var accountDto = new UserAccountDto()
            {
                UserId = entity.UserId,
                accountId = entity.AccountId,
                accountLink = entity.AccountLink,
            };
            return accountDto;
        }
    }
}