using Microsoft.EntityFrameworkCore;
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
        public async override Task<IQueryable<Student>> GetAllAsync()
        {
            var Query = await _context.Students.Where(x => !x.IsDeleted).ToListAsync();
            return Query.AsQueryable();
        }
        public IQueryable<Student> GetRecentStudentEnrollmentAsync(int recentNumber, int courseId)
        {
            var recentStudents = _context.Students
                                       .Include(std => std.Enrollments)
                                       .Where(std => std.Enrollments.Any(enrollment => enrollment.CourseId == courseId) && std.ProfilePicture != null && !std.IsDeleted)
                                       .OrderByDescending(c => c.CreatedAt)
                                       .Take(recentNumber);
            return recentStudents.AsQueryable();
        }


    }
}