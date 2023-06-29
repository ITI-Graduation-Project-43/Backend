using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IDiscussionRepository : IRepository<Discussion, int>
    {

        IQueryable<Discussion> GetAllDiscussionByLessonIdAsync(int lessonId, int pageNumber, int pageSize);

        IQueryable<Discussion> GetAllDiscussionByParentIdAsync(int parentId, int pageNumber, int pageSize);
        Task<IEnumerable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId);

    }
}