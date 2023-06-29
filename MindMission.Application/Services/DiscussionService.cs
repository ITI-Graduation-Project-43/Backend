using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
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


        public Task<IEnumerable<Discussion>> GetAllDiscussionByLessonIdAsync(int lessonId)
        {
            return _context.GetAllDiscussionByLessonIdAsync(lessonId);
        }


        public async Task<IEnumerable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId)
        {
            return await _context.GetAllDiscussionByParentIdAsync(parentId);
        }


    }
}