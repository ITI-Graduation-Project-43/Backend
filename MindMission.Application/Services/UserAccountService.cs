using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class UserAccountService : Service<UserAccount, int>, IUserAccountService
    {
        private readonly IUserAccountRepository _context;

        public UserAccountService(IUserAccountRepository context) : base(context)
        {
            _context = context;
        }



        public async Task<IQueryable<UserAccount>> GetUserAccountsAsync(string id)
        {
            return await _context.GetUserAccountsAsync(id);
        }


    }
}