using Microsoft.EntityFrameworkCore;
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

        public async Task<IQueryable<Discussion>> GetAllDiscussionByLessonIdAsync(int lessonId)
        {
            return (IQueryable<Discussion>)await Context.Discussions.Include(d => d.ParentDiscussion).Where(e => e.LessonId == lessonId && !e.IsDeleted).OrderByDescending(d => d.CreatedAt).ToListAsync();
        }

        public async Task<IQueryable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId)
        {

            return (IQueryable<Discussion>)await Context.Discussions.Include(d => d.ParentDiscussion).Where(d => d.ParentDiscussionId == parentId && !d.IsDeleted).OrderByDescending(d => d.CreatedAt).ToListAsync();
        }


    }
}