using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Services_Classes
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
    }
}
