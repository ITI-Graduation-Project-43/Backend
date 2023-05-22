using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.Infrastructure.Repositories
{
    public class PermissionRepository : Repository<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(MindMissionDbContext context) : base(context)
        {
        }
    }
}
