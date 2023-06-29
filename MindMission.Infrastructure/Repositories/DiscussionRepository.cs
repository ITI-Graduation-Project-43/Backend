using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class DiscussionRepository : Repository<Discussion, int>, IDiscussionRepository
    {
        private readonly MindMissionDbContext _context;

        public DiscussionRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }
        public async override Task<IQueryable<Discussion>> GetAllAsync()
        {
            var Query = await _context.Discussions.Where(x => !x.IsDeleted).ToListAsync();
            return Query.AsQueryable();
        }
        public async Task<IEnumerable<Discussion>> GetAllDiscussionByLessonIdAsync(int lessonId)
        {
            return await _context.Discussions
                                    .Include(d => d.ParentDiscussion)
                                    .Include(d => d.User)
                                    .Where(e => e.LessonId == lessonId && !e.IsDeleted)
                                    .OrderByDescending(d => d.CreatedAt).ToListAsync();

        }

        public async Task<IEnumerable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId)
        {

            return await _context.Discussions.Include(d => d.ParentDiscussion).Where(d => d.ParentDiscussionId == parentId && !d.IsDeleted).OrderByDescending(d => d.CreatedAt).ToListAsync();
        }


    }
}