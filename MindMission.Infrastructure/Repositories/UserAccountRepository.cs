using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Infrastructure.Repositories
{
    public class UserAccountRepository:Repository<UserAccount,int>, IUserAccountRepository
    {
        private readonly MindMissionDbContext _context;
        public UserAccountRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }

        
        List<UserAccount> IUserAccountRepository.GetUserAccountsAsync(string id)
        {
            return _context.UserAccounts.Include(i => i.Account).Where(i => i.UserId == id).ToList();
        }
    }
}
