using MindMission.Application.DTOs;
using MindMission.Application.DTOs.UserAccount;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping
{
    public class UpdateUserAccountDto : IMappingService<UserAccount, UserAccountsDto>
    {
        public UserAccount MapDtoToEntity(UserAccountsDto dto)
        {
            return new UserAccount();
        }

        public async Task<UserAccountsDto> MapEntityToDto(UserAccount entity)
        {
            var accountDto = new UserAccountsDto();
            return accountDto;
        }

    }
}
