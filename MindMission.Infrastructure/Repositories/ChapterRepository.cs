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

        public IQueryable<Chapter> GetByCourseIdAsync(int courseId, int pageNumber, int pageSize)
        {
            var Chapter = _context.Chapters
                       .Include(chapter => chapter.Lessons)
                       .Where(chapter => chapter.CourseId == courseId && !chapter.IsDeleted)
                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return Chapter.AsQueryable();
        }
    }
}