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
            var userAccounts = await _dbSet.AsSplitQuery().Include(i=>i.Account).Where(w => w.UserId == UserId && !w.IsDeleted).ToListAsync();
            return userAccounts.AsQueryable();
        }
        public async Task<UserAccount> GetUserAccountByUserIdAndAccountId(string userId, int accountId)
        {
            var existingAccount = _context.UserAccounts.FirstOrDefault(a =>
            a.UserId == userId && a.AccountId == accountId);

            return existingAccount;
        }
        public async Task<UserAccountDto> UpdateUserAccount(string userId, int accountId, string accountLink)
        {
           UserAccount existingAccount = await GetUserAccountByUserIdAndAccountId(userId, accountId);

            if (existingAccount == null)
            {
                existingAccount = new UserAccount
                {
                    UserId = userId,
                    AccountId = accountId,
                    AccountLink = accountLink
                };
                _dbSet.Add(existingAccount);
            }
            else
            {
                existingAccount.AccountLink = accountLink;
                _dbSet.Update(existingAccount);
            }
            await _context.SaveChangesAsync();
            var dto = new UserAccountDto
            {
                UserId = existingAccount.UserId,
                AccountId = existingAccount.AccountId,
                AccountLink = existingAccount.AccountLink
            };
            return dto;
        }

    }
}