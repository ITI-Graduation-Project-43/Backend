using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services.Base;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Services
{
    public interface IAccountService : IService<Account, int>, IAccountRepository
    {
    }
}
