using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class AdminRepository : Repository<Admin, int>, IAdminRepository
    {

        public AdminRepository(MindMissionDbContext context) : base(context)
        {
        }



    }
}