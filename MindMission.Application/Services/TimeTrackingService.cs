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

        public IQueryable<TimeTracking> GetByCourseId(int CourseId, int pageNumber, int pageSize)
        {
            return  _repository.GetByCourseId(CourseId, pageNumber, pageSize);
        }

        public IQueryable<TimeTracking> GetByStudentId(string StudentId, int pageNumber, int pageSize)
        {
            return _repository.GetByStudentId(StudentId, pageNumber, pageSize);
        }

        public async Task<TimeTracking> Update(string studentId, int courseId)
        {
            return  await _repository.Update(studentId, courseId);
        }
        public  IQueryable<Student> GetLastfourStudentIds(int courseId)
        {
            return  _repository.GetLastfourStudentIds(courseId);
        }
        public  object GetCourseVisitCount(int courseId, int pageNumber, int pageSize)
        {
            var courseVisits =  _repository.GetByCourseId(courseId, pageNumber, pageSize);
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
        
        public async Task<long> GetTotalHours (string instructorId, int pageNumber, int pageSize)
        {
            long totalHourSpent = 0;
            var totalCourses = await _courseRepository.GetAllByInstructorAsync(instructorId);
            var courseIds = totalCourses.Select(course => course.Id).ToList();
            var timeTracks =  _repository.GetByCourseIds(courseIds,pageNumber,pageSize);

            foreach (var course in totalCourses)
            {
                long hourSpent = 0;
                var courseTimeTracks = timeTracks.Where(track => track.CourseId == course.Id);
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

        public IQueryable<TimeTracking> GetByCourseIds(List<int> courseIds, int pageNumber, int pageSize)
        {
            return  _repository.GetByCourseIds(courseIds, pageNumber, pageSize);
        }
    }
}
