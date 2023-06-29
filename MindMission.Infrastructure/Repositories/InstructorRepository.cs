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

        public async override Task<IQueryable<Instructor>> GetAllAsync()
        {
            var Query = await _context.Instructors.Where(x => !x.IsDeleted).ToListAsync();
            return Query.AsQueryable();
        }
        public async Task<IQueryable<Instructor>> GetTopRatedInstructorsAsync(int topNumber)
        {
            var topInstructors = await _context.Instructors
                                            .AsSplitQuery()
                                            .Include(ins => ins.Courses)
                                            .Where(i => !i.IsDeleted)
                                           .OrderByDescending(i => i.AvgRating)
                                           .Take(topNumber)
                                           .ToListAsync();
            return topInstructors.AsQueryable();
        }

        public async Task<int> GetTotalTopRatedInstructorsAsync()
        {
            return _context.Instructors.Count(e => e.AvgRating > 3.5);
        }

    }
}