using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account, int>, IAccountRepository
    {

        public AccountRepository(MindMissionDbContext context) : base(context)
        {
        }
    }
}
