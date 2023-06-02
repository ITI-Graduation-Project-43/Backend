using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IUserAccountRepository : IRepository<UserAccount, int>
    {
        List<UserAccount> GetUserAccountsAsync(string id);
    }
}