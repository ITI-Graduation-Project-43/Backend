using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IAccountRepository : IRepository<Account, int>
    {
    }

}
