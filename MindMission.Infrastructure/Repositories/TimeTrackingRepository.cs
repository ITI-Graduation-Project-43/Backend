using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using System.Formats.Asn1;

namespace MindMission.Infrastructure.Repositories
{
    public class TimeTrackingRepository : ITrackingTimeRepository
    {
        private readonly MindMissionDbContext _context;
        public TimeTrackingRepository(MindMissionDbContext context)
        {
            _context= context;
        }
        public async Task<IQueryable<TimeTracking>> GetAll()
        {
            return await Task.FromResult(_context.Set<TimeTracking>());
        }
        public async Task<TimeTracking> Create(string studentId,int courseId)
        {
            var timeTracking = new TimeTracking
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = courseId,
                StartTime = DateTime.Now,
            };
            _context.TimeTrackings.Add(timeTracking);
            await _context.SaveChangesAsync();
            return timeTracking;
        }

        public async Task<List<Student>> GetLastfourStudentIds()
        {
            var students = await _context.TimeTrackings.Include(e => e.Student).Where(cv => cv.EndTime != null)
            .Select(cv => cv.Student)
            .Distinct()
            .OrderByDescending(studentId => studentId)
            .Take(4)
            .ToListAsync();

            return students;
        }

        public async Task<IQueryable<TimeTracking>> GetByCourseId(int CourseId)
        {
            var TimeTrackings = _context.TimeTrackings.Include(e => e.Student).Include(e => e.Course).Where(e => e.CourseId == CourseId);
            return await (Task<IQueryable<TimeTracking>>)TimeTrackings.AsQueryable();
        }

        public async Task<IQueryable<TimeTracking>> GetByStudentId(string StudentId)
        {
            var TimeTrackings = _context.TimeTrackings.Include(e => e.Student).Include(e => e.Course).Where(e => e.StudentId == StudentId);
            return await (Task<IQueryable<TimeTracking>>)TimeTrackings.AsQueryable();
        }

        public async Task<TimeTracking> Update(string studentId, int courseId)
        {
            var timeTracking = await _context.TimeTrackings.FirstOrDefaultAsync(cv =>
            cv.StudentId == studentId &&
            cv.CourseId == courseId &&
            cv.EndTime == null);

            if (timeTracking == null)
            {
                return null;
            }

            timeTracking.EndTime = DateTime.Now;

            await _context.SaveChangesAsync();

            return timeTracking;
        }
    }
}
