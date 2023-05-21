using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Exceptions;

namespace MindMission.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        private readonly MindMissionDbContext _context;

        public CourseRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;

        }


        public async Task<Course> GetByNameAsync(string name)
        {

            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Title == name);

            if (course == null)
            {
                throw new EntityNotFoundException($"No course with the title {name} found.");
            }

            return course;

        }

    }
}
