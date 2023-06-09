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

        public async Task<IQueryable<Student>> GetRecentStudentEnrollmentAsync(int recentNumber, int courseId)
        {
            var recentStudents = await _context.Students
                                       .Include(std => std.Enrollments)
                                       .Where(std => std.Enrollments.Any(enrollment => enrollment.CourseId == courseId) && std.ProfilePicture != null)
                                       .OrderByDescending(c => c.CreatedAt)
                                       .Take(recentNumber)
                                       .ToListAsync();
            return recentStudents.AsQueryable();
        }


    }
}