using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class DiscussionRepository : Repository<Discussion, int>, IDiscussionRepository
    {
        private readonly MindMissionDbContext Context;

        public DiscussionRepository(MindMissionDbContext _Context) : base(_Context)
        {
            Context = _Context;
        }

        public Task<IEnumerable<Discussion>> GetAllDiscussionByLessonIdAsync(int id)
        {
            IEnumerable<Discussion> result = Context.Discussions.Where(e => e.LessonId == id);
            return (Task<IEnumerable<Discussion>>)result;
        }
    }
}