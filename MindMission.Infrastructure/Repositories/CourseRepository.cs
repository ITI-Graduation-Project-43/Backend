using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

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

        public async Task<IQueryable<Course>> GetAllByCategoryAsync(int categoryId)
        {
            return (IQueryable<Course>) await _context.Courses
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .Include(c => c.Category)
                         .Where(c => c.CategoryId == categoryId)
                         .ToListAsync();
        }

        public async Task<IQueryable<Course>> GetRelatedCoursesAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            return course == null
                ? throw new Exception($"Course with id {courseId} not found.")
                : (IQueryable<Course>)await _context.Courses
                             .Include(c => c.Instructor)
                             .Include(c => c.Chapters)
                             .Include(c => c.Category)
                             .Where(c => c.CategoryId == course.CategoryId && c.Id != courseId)
                             .ToListAsync();
        }

        public async Task<IQueryable<Course>> GetAllByInstructorAsync(string instructorId)
        {
            var Query = await _context.Courses
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .Include(c => c.Category)
                         .Where(c => c.InstructorId == instructorId)
                         .ToListAsync();

            return Query.AsQueryable();
        }

        public async Task<IQueryable<Course>> GetTopRatedCoursesAsync(int topNumber)
        {
            var Query = await _context.Courses
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .Include(c => c.Category)
                         .OrderByDescending(c => c.AvgReview)
                         .Take(topNumber)
                         .ToListAsync();

            return Query.AsQueryable();
        }

        public async Task<IQueryable<Course>> GetRecentCoursesAsync(int recentNumber)
        {
            var Query = await _context.Courses
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .Include(c => c.Category)
                         .OrderByDescending(c => c.CreatedAt)
                         .Take(recentNumber)
                         .ToListAsync();

            return Query.AsQueryable();
        }
    }
}