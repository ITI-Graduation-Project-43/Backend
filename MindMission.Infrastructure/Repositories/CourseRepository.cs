using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

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
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var entity = await _context.Courses
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .Include(c => c.Category)
                            .FirstOrDefaultAsync(c => c.Title.ToLower() == name.ToLower());

            return entity ?? throw new KeyNotFoundException($"No entity with name {name} found.");
        }

        public async Task<IEnumerable<Course>> GetAllByCategoryAsync(int categoryId)
        {
            return await _context.Courses
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .Include(c => c.Category)
                         .Where(c => c.CategoryId == categoryId)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetRelatedCoursesAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            return course == null
                ? throw new Exception($"Course with id {courseId} not found.")
                : (IEnumerable<Course>)await _context.Courses
                             .Include(c => c.Instructor)
                             .Include(c => c.Chapters)
                             .Include(c => c.Category)
                             .Where(c => c.CategoryId == course.CategoryId && c.Id != courseId)
                             .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllByInstructorAsync(string instructorId)
        {
            return await _context.Courses
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .Include(c => c.Category)
                         .Where(c => c.InstructorId == instructorId)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetTopRatedCoursesAsync(int topNumber)
        {
            return await _context.Courses
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .Include(c => c.Category)
                         .OrderByDescending(c => c.AvgReview)
                         .Take(topNumber)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetRecentCoursesAsync(int recentNumber)
        {
            return await _context.Courses
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .Include(c => c.Category)
                         .OrderByDescending(c => c.CreatedAt)
                         .Take(recentNumber)
                         .ToListAsync();
        }



    }
}
