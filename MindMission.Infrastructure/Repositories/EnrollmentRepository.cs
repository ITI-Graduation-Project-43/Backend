using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.Infrastructure.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment, int> , IEnrollmentRepository
    {
        private readonly MindMissionDbContext _context;
        private readonly DbSet<Enrollment> _dbSet;
        public EnrollmentRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Enrollment>();
        }

        public async Task<IQueryable<Enrollment>> GetAllByCourseIdAsync(int CourseId)
        {
            var Enrollments = await _dbSet.Where(w => w.CourseId == CourseId).ToListAsync();
            return Enrollments.AsQueryable();
        }

        public async Task<IQueryable<Enrollment>> GetAllByStudentIdAsync(string StudentId)
        {
            var Enrollments = await _dbSet.Where(w => w.StudentId == StudentId).ToListAsync();
            return Enrollments.AsQueryable();
        }
    }
}
