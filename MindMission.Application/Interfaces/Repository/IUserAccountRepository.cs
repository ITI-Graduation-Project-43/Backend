﻿using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IUserAccountRepository : IRepository<UserAccount, int>
    {
        Task<IQueryable<UserAccount>> GetUserAccountsAsync(string id);
        IQueryable<UserAccount> GetAllByUserIdAsync(string UserId, int pageNumber, int pageSize);
        Task<IQueryable<UserAccount>> UpdateUserAccount(List<UserAccount> _UserAccounts);

    }
}