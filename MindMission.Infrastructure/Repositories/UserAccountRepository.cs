using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Common;
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
            var userAccounts = _dbSet.AsSplitQuery().Include(i => i.Account).Where(w => w.UserId == UserId && !w.IsDeleted);
            return userAccounts.AsQueryable();
        }
        
        public async Task<IQueryable<UserAccount>> GetUserAccountByUserIdAndAccountId(string userId)
        {
            //var existingAccount = _context.UserAccounts.FirstOrDefault(a => a.UserId == userId && a.AccountId == accountId);

            var existingAccount = _context.UserAccounts.Where(a => a.UserId == userId);


            return existingAccount;
        }

        public async Task<IQueryable<UserAccount>> UpdateUserAccount(List<UserAccount> _UserAccounts)
        {
            _context.UserAccounts.UpdateRange(_UserAccounts);
            await _context.SaveChangesAsync();
            return _UserAccounts.AsQueryable();
        }
    }
}