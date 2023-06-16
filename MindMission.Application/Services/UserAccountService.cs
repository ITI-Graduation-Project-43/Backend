using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _context;

        public UserAccountService(IUserAccountRepository context)
        {
            _context = context;
        }

        public Task<UserAccount> AddAsync(UserAccount entity)
        {
            return _context.AddAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }
        public Task SoftDeleteAsync(int id)
        {
            return _context.SoftDeleteAsync(id);
        }

        public Task<IQueryable<UserAccount>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public async Task<IEnumerable<UserAccount>> GetAllAsync(params Expression<Func<UserAccount, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<UserAccount> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<UserAccount> GetByIdAsync(int id, params Expression<Func<UserAccount, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }

        public async Task<IQueryable<UserAccount>> GetUserAccountsAsync(string id)
        {
            return await _context.GetUserAccountsAsync(id);
        }

        public Task<UserAccount> UpdateAsync(UserAccount entity)
        {
            return _context.UpdateAsync(entity);
        }
        public async Task<UserAccount> UpdatePartialAsync(int id, UserAccount entity)
        {
            return await _context.UpdatePartialAsync(id, entity);
        }
    }
}