using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student, string>, IStudentRepository
    {
        private readonly MindMissionDbContext _context;

        public StudentRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }
    }
}