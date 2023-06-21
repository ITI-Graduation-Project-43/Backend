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
        private readonly DbSet<UserAccount> _dbSet;

        public UserAccountRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<UserAccount>();
        }

        public async Task<IQueryable<UserAccount>> GetUserAccountsAsync(string id)
        {
            var userAccounts = await _context.UserAccounts.Include(i => i.Account).Where(i => i.UserId == id && !i.IsDeleted).ToListAsync();
            return userAccounts.AsQueryable();
        }
        public async Task<IQueryable<UserAccount>> GetAllByUserIdAsync(string UserId)
        {
            var userAccounts = await _dbSet.AsSplitQuery().Include(i=>i.Account).Where(w => w.UserId == UserId && !w.IsDeleted).ToListAsync();
            return userAccounts.AsQueryable();
        }

    }
}