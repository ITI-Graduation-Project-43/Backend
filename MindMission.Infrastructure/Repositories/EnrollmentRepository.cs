using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment, int>, IEnrollmentRepository
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
            var Enrollments = await _dbSet.Where(w => w.CourseId == CourseId && !w.IsDeleted).ToListAsync();
            return Enrollments.AsQueryable();
        }

        public async Task<IQueryable<Enrollment>> GetAllByStudentIdAsync(string StudentId)
        {
            var Enrollments = await _dbSet.Where(w => w.StudentId == StudentId && !w.IsDeleted).ToListAsync();
            return Enrollments.AsQueryable();
        }

        public async Task<Enrollment> GetByStudentAndCourseAsync(string StudentId, int CourseId)
        {
            return await _dbSet.Where(w => w.StudentId == StudentId && w.CourseId == CourseId && !w.IsDeleted).FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException($"There is no enrollment with courseId {CourseId} and studentId {StudentId}");
        }

        public async Task<int> SuccessfulLearners()
        {
            return  _dbSet.Count();
        }
    }
}