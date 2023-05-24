using MindMission.Application.DTOs;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
