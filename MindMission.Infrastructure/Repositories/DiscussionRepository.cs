using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Infrastructure.Repositories
{
    public class DiscussionRepository : Repository<Discussion, int>, IDiscussionRepository
    {
        private readonly MindMissionDbContext Context;
        public DiscussionRepository(MindMissionDbContext _Context) : base(_Context)
        {
            Context = _Context;
        }

        public async Task<IEnumerable<Discussion>> GetAllDiscussionByLessonIdAsync(int lessonId)
        {
            return await Context.Discussions.Include(d=>d.ParentDiscussion).Where(e => e.LessonId == lessonId).OrderByDescending(d=>d.CreatedAt).ToListAsync();  
        }

        public async Task<IEnumerable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId)
        {
            return await Context.Discussions.Include(d=>d.ParentDiscussion).Where(d=>d.ParentDiscussionId==parentId).OrderByDescending(d => d.CreatedAt).ToListAsync();
        }
    }
}
