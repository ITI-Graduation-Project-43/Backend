using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class UserAccountRepository : Repository<UserAccount, int>, IUserAccountRepository
    {
        private readonly MindMissionDbContext _context;

        public UserAccountRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<UserAccount>> GetUserAccountsAsync(string id)
        {
            var userAccounts = await _context.UserAccounts.Include(i => i.Account).Where(i => i.UserId == id && !i.IsDeleted).ToListAsync();
            return userAccounts.AsQueryable();
        }

    }
}