using Microsoft.Identity.Client;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
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
                AccountId = dto.AccountId,
                AccountLink = dto.AccountLink,
            };
        }

        public async Task<UserAccountDto> MapEntityToDto(UserAccount entity)
        {
            var accountDto = new UserAccountDto()
            {
                UserId = entity.UserId,
                AccountId = entity.AccountId,
                AccountLink = entity.AccountLink,
                AccountName = entity.Account.AccountName
            };
            
            return accountDto;
        }
    }
}