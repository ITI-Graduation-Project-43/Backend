using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Services
{
    public interface IUserService : IRepository<User, string>
    {
    }
}
