using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Common;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using System.Drawing.Printing;
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
        public async Task<IEnumerable<TimeTracking>> GetAll()
        {
            return await Task.FromResult(_context.Set<TimeTracking>());
        }
        public async Task<TimeTracking> Create(string studentId,int courseId)
        {
            var timeTrack = _context.TimeTrackings
                .Where(e => e.CourseId == courseId && e.StudentId == studentId && e.EndTime == null).FirstOrDefault();
            if (timeTrack == null)
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
            else
            {
                timeTrack.StartTime = DateTime.Now;
                _context.TimeTrackings.Add(timeTrack);
                await _context.SaveChangesAsync();
                return timeTrack;
            }
             
        }

        public async Task<List<Student>> GetLastfourStudentIds(int courseId)
        {
            List<Student> students =  new List<Student>();
            var timetracks = await _context.TimeTrackings
                                .Where(cv => cv.CourseId == courseId && cv.EndTime != null)
                                .OrderByDescending(s => s.EndTime).ToListAsync();
            var studentIds= timetracks.Select(cv => cv.StudentId).Distinct().Take(4);
            foreach (var stdId in studentIds)
            {
                Student student =  _context.Students.FirstOrDefault(s => s.Id == stdId);
                 students.Add(student);
            }
            return students;
        }

        public async Task<IEnumerable<TimeTracking>> GetByCourseIds(List<int> courseIds)
        {
            var timeTrackings = await _context.TimeTrackings.Where(e => courseIds.Contains(e.CourseId)).ToListAsync();
            return timeTrackings;
        }
        public async Task<IEnumerable<TimeTracking>> GetByCourseId(int CourseId)
        {
            var TimeTrackings = _context.TimeTrackings.Where(e => e.CourseId == CourseId).ToList();
                
            return TimeTrackings;
        }

        public async Task<IEnumerable<TimeTracking>> GetByStudentId(string StudentId)
        {
            var TimeTrackings = await _context.TimeTrackings.Where(e => e.StudentId == StudentId).ToListAsync();
            return  TimeTrackings;
        }

        public async Task<TimeTracking> Update(string studentId, int courseId)
        {
            var timeTracking = await _context.TimeTrackings.FirstOrDefaultAsync(cv =>
            cv.StudentId == studentId &&
            cv.CourseId == courseId &&
            cv.EndTime.HasValue == false);

            if (timeTracking == null)
            {
                return null;
            }

            timeTracking.EndTime = DateTime.Now;
            _context.Set<TimeTracking>().Update(timeTracking);

            await _context.SaveChangesAsync();

            return timeTracking;
        }
    }
}
