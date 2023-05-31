using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IDiscussionRepository : IRepository<Discussion, int>
    {
        Task<IQueryable<Discussion>> GetAllDiscussionByLessonIdAsync(int lessonId);
        Task<IQueryable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId);
    }
}