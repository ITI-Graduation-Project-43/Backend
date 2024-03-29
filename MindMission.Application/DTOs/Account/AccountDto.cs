﻿using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs.Account
{
    public class AccountDto : IDtoWithId<int>
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        
        public string AccountName { get; set; } = string.Empty;

        public string AccountDomain { get; set; } = string.Empty;

    }
}
