using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.Infrastructure.Repositories
{
    public class InstructorRepository : Repository<Instructor, string>, IInstructorRepository
    {
        public InstructorRepository(MindMissionDbContext context) : base(context)
        {
        }
    }
}
