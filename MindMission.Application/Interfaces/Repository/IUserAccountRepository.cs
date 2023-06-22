using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IUserAccountRepository : IRepository<UserAccount, int>
    {
        Task<IQueryable<UserAccount>> GetUserAccountsAsync(string id);
        Task<IQueryable<UserAccount>> GetAllByUserIdAsync(string UserId);
        Task<UserAccountDto> UpdateUserAccount(string userId, int accountId, string accountLink);

    }
}