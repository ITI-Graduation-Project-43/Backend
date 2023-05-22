using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, string>, IUserRepository
    {
        public UserRepository(MindMissionDbContext context) : base(context)
        {
        }
    }
}
