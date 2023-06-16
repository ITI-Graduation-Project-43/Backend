﻿using MindMission.Application.Interfaces.Repository;


namespace MindMission.Application.Interfaces.Services
{
    public  interface ITimeTrackingService : ITrackingTimeRepository
    {
        Task<IEnumerable<object>> GetCourseVisitCountByHour(int courseId);
        Task<long> getTotalHours(string instructorId);
    }

}