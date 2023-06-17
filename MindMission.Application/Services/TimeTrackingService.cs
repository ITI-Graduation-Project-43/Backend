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
            return await _repository.Update(studentId, courseId);
        }
        public async Task<List<Student>> GetLastfourStudentIds(int courseId)
        {
            return await _repository.GetLastfourStudentIds(courseId);
        }
        public async Task<IEnumerable<object>> GetCourseVisitCountByHour(int courseId)
        {
            var courseVisits = await _repository.GetAll();

            var countPerHour = courseVisits
                .Where(cv => cv.CourseId == courseId)
                .GroupBy(cv => cv.StartTime.Value.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() });
            return countPerHour;
        }
        public async Task<long> getTotalHours (string instructorId)
        {
            long totalHourSpent = 0;
            var totalCourses = await _courseRepository.GetAllByInstructorAsync(instructorId);
             
            foreach (var course in totalCourses)
            {
                long hourSpent = 0;
                var timeTrack = await _repository.GetByCourseId(course.Id);
                if (timeTrack != null) {
                    foreach (var time in timeTrack)
                    {
                        long timeSpent = ((time.EndTime.Value.Hour - time.StartTime.Value.Hour)*60 + (time.EndTime.Value.Minute - time.StartTime.Value.Minute))/60;
                        hourSpent += timeSpent;
                    }
                }
                totalHourSpent += hourSpent;
            }

                return totalHourSpent;
        }
    }
}
