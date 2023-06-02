using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services_Classes
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _context;

        public QuestionService(IQuestionRepository context)
        {
            _context = context;
        }

        public Task<IQueryable<Question>> GetAllAsync()
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

        public async Task<IEnumerable<Question>> GetAllAsync(params Expression<Func<Question, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Question> GetByIdAsync(int id, params Expression<Func<Question, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }
    }
}