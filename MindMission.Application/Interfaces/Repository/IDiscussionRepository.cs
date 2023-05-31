using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IDiscussionRepository : IRepository<Discussion, int>
    {
        Task<IEnumerable<Discussion>> GetAllDiscussionByLessonIdAsync(int lessonId);
        Task<IEnumerable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId);
    }
}