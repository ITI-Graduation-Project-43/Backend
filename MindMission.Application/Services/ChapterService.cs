using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Services_Classes
{
    public class ChapterService : Service<Chapter, int>, IChapterService
    {
        private readonly IChapterRepository _context;

        public ChapterService(IChapterRepository context) : base(context)
        {
            _context = context;
        }


        public IQueryable<Chapter> GetByCourseIdAsync(int courseId, int pageNumber, int pageSize)
        {
            return _context.GetByCourseIdAsync(courseId, pageNumber, pageSize);
        }

    }
}