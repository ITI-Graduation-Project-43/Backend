using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<Course> GetByNameAsync(string name);

    }
}
