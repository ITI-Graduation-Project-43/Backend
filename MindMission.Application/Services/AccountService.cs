using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class AccountService : Service<Account, int>, IAccountService
    {
        public AccountService(IAccountRepository context) : base(context)
        {
        }

    }
}
