using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository Context;
        public UserService(IUserRepository _Context)
        {
            Context = _Context;
        }
        public Task<IEnumerable<User>> GetAllAsync()
        {
            return Context.GetAllAsync();
        }

        public Task<User> GetByIdAsync(string id)
        {
            return Context.GetByIdAsync(id);
        }

        public Task<User> AddAsync(User entity)
        {
            return Context.AddAsync(entity);
        }

        public Task UpdateAsync(User entity)
        {
            return Context.UpdateAsync(entity);
        }

        public Task DeleteAsync(string id)
        {
            return Context.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync(params Expression<Func<User, object>>[] IncludeProperties)
        {
            return await Context.GetAllAsync(IncludeProperties);
        }

        public async Task<User> GetByIdAsync(string id, params Expression<Func<User, object>>[] IncludeProperties)
        {
            return await Context.GetByIdAsync(id,IncludeProperties);
        }
    }
}
