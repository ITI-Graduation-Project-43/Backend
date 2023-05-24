﻿using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<IEnumerable<UserAccount>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<UserAccount> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public List<UserAccount> GetUserAccountsAsync(string id)
        {
            return _context.GetUserAccountsAsync(id);
        }

        public Task UpdateAsync(UserAccount entity)
        {
            return _context.UpdateAsync(entity);
        }
    }
}
