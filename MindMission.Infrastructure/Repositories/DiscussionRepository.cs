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

        public override IQueryable<Discussion> GetAllAsync(int pageNumber, int pageSize)
        {

            var Query = _context.Discussions.Include(d => d.ParentDiscussion)
                                            .Include(d => d.User)
                                            .Where(x => !x.IsDeleted).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return Query.AsQueryable();
        }
        public IQueryable<Discussion> GetAllDiscussionByLessonIdAsync(int lessonId, int pageNumber, int pageSize)
        {
            return _context.Discussions
                                    .Include(d => d.ParentDiscussion)
                                    .Include(d => d.User)
                                    .Where(e => e.LessonId == lessonId && !e.IsDeleted)
            .OrderByDescending(d => d.CreatedAt).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<Discussion> GetAllDiscussionByParentIdAsync(int parentId, int pageNumber, int pageSize)
        {

            return _context.Discussions.Include(d => d.ParentDiscussion).Where(d => d.ParentDiscussionId == parentId && !d.IsDeleted).OrderByDescending(d => d.CreatedAt).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public async Task<IEnumerable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId)
        {

            return await _context.Discussions.Include(d => d.ParentDiscussion).Where(d => d.ParentDiscussionId == parentId && !d.IsDeleted).OrderByDescending(d => d.CreatedAt).ToListAsync();
        }

    }
}