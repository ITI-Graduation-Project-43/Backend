using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IChapterRepository : IRepository<Chapter, int>
    {
        Task<IQueryable<Chapter>> GetByCourseIdAsync(int courseId);

    }
}