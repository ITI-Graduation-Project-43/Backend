using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly ITrackingTimeRepository _repository;
        private readonly ICourseRepository _courseRepository;
        public TimeTrackingService(ITrackingTimeRepository repository,ICourseRepository courseRepository)
        {
            _repository = repository;
            _courseRepository = courseRepository;
        }
        public async Task<IEnumerable<TimeTracking>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<TimeTracking> Create(string studentId, int courseId)
        {
            return await _repository.Create(studentId, courseId);
        }

        public async Task<IEnumerable<TimeTracking>> GetByCourseId(int CourseId)
        {
            return await _repository.GetByCourseId(CourseId);
        }

        public async Task<IEnumerable<TimeTracking>> GetByStudentId(string StudentId)
        {
            return await _repository.GetByStudentId(StudentId);
        }

        public async Task<TimeTracking> Update(string studentId, int courseId)
        {
            return  await _repository.Update(studentId, courseId);
        }
        public async Task<List<Student>> GetLastfourStudentIds(int courseId)
        {
            return await _repository.GetLastfourStudentIds(courseId);
        }
        public async Task<object> GetCourseVisitCount(int courseId)
        {
            var courseVisits = await _repository.GetByCourseId(courseId);
            var hourlyCounts = Enumerable.Range(0, 24)
            .Select(hour => new { Hour = hour, Count = courseVisits.Count(log => log.StartTime.Value.Hour == hour) })
            .ToArray();
            var dailyCounts = new[] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday }
            .Select(day => new { Day = day.ToString(), Count = courseVisits.Count(log => log.StartTime.Value.DayOfWeek == day) })
            .ToArray();
            var monthlyCounts = Enumerable.Range(1, 12)
            .Select(month => new { Month = month, Count = courseVisits.Count(log => log.StartTime.Value.Month == month) })
            .ToArray();

            var result = new object[] { hourlyCounts, dailyCounts, monthlyCounts };
/*
            var countPerHour = courseVisits
                .GroupBy(cv => cv.StartTime.Value.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() });
*/
            return result;
        }

        public async Task<long> GetTotalHours(string instructorId)
        {
            long totalHourSpent = 0;
            var totalCourses = await _courseRepository.GetAllByInstructorAsync(instructorId);
            var courseIds = totalCourses.Select(course => course.Id).ToList();
            var timeTracks = await _repository.GetByCourseIds(courseIds);

            foreach (var course in totalCourses)
            {
                long hourSpent = 0;
                var courseTimeTracks = timeTracks.Where(track => track.CourseId == course.Id).ToList();
                if (courseTimeTracks != null) {
                    foreach (var time in courseTimeTracks)
                    {
                        long timeSpent = ((time.EndTime.Value.Hour - time.StartTime.Value.Hour)*60 + (time.EndTime.Value.Minute - time.StartTime.Value.Minute))/60;
                        hourSpent += timeSpent;
                    }
                }
                totalHourSpent += hourSpent;
            }

                return totalHourSpent;
        }

        public async Task<IEnumerable<TimeTracking>> GetByCourseIds(List<int> courseIds)
        {
            return await _repository.GetByCourseIds(courseIds);
        }
    }
}
