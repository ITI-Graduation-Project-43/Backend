using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.Infrastructure.Repositories
{
    public class AdminRepository : Repository<Admin,int>,IAdminRepository
    {
        private readonly MindMissionDbContext _context;
        public AdminRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
