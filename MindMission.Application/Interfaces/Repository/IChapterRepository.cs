using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IChapterRepository : IRepository<Chapter, int>
    {
        public Task<IQueryable<Chapter>> GetByCourseIdAsync(int courseId);

    }
}