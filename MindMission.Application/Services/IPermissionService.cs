using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public interface IPermissionService : IRepository<Permission, int>
    {

    }
}
