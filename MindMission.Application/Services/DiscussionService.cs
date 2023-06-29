using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class DiscussionService : Service<Discussion, int>, IDiscussionService
    {
        private readonly IDiscussionRepository _context;

        public DiscussionService(IDiscussionRepository context) : base(context)
        {
            _context = context;
        }


        public IQueryable<Discussion> GetAllDiscussionByLessonIdAsync(int lessonId, int pageNumber, int pageSize)
        {
            return _context.GetAllDiscussionByLessonIdAsync(lessonId, pageNumber, pageSize);
        }


        public IQueryable<Discussion> GetAllDiscussionByParentIdAsync(int parentId, int pageNumber, int pageSize)
        {
            return _context.GetAllDiscussionByParentIdAsync(parentId, pageNumber, pageSize);
        }

        public Task<IEnumerable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId)
        {
            return _context.GetAllDiscussionByParentIdAsync(parentId);
        }
    }
}