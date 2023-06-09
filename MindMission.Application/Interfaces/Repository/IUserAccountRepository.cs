using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IUserAccountRepository : IRepository<UserAccount, int>
    {
        Task<IQueryable<UserAccount>> GetUserAccountsAsync(string id);

    }
}