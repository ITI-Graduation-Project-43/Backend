using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class ChapterRepository : Repository<Chapter, int>, IChapterRepository
    {
        private readonly MindMissionDbContext _context;

        public ChapterRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<Chapter>> GetByCourseIdAsync(int courseId)
        {
            var Chapter = await _context.Chapters
                       .Include(chapter => chapter.Lessons)
                       .Where(chapter => chapter.CourseId == courseId && !chapter.IsDeleted)
                       .ToListAsync();

            return Chapter.AsQueryable();
        }
    }
}