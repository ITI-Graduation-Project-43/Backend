using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class InstructorRepository : Repository<Instructor, string>, IInstructorRepository
    {
        private readonly MindMissionDbContext _context;

        public InstructorRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Instructor> GetAllAsync(int pageNumber, int pageSize)
        {

            var Query = _context.Instructors.AsSplitQuery().Include(instructor => instructor.User).Include(Instructor => Instructor.Courses).Where(x => !x.IsDeleted).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return Query.AsQueryable();
        }
        public IQueryable<Instructor> GetTopRatedInstructorsAsync(int topNumber, int pageNumber, int pageSize)
        {
            var topInstructors = _context.Instructors
                                            .AsSplitQuery()
                                            .Include(ins => ins.Courses)
                                            .Where(i => !i.IsDeleted)
                                           .OrderByDescending(i => i.AvgRating)
                                           .Take(topNumber)
                                           .Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return topInstructors.AsQueryable();
        }

        public async Task<int> GetTotalTopRatedInstructorsAsync()
        {
            return _context.Instructors.Count(e => e.AvgRating > 3.5);
        }

    }
}