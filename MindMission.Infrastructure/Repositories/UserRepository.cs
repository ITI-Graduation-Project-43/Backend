using MindMission.Application.Interfaces.Repository;
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
