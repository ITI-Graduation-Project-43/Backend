using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Services_Classes
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _context;

        public QuestionService(IQuestionRepository context)
        {
            _context = context;
        }

        public Task<IEnumerable<Question>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Question> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Question> AddAsync(Question entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Question entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }
    }
}
