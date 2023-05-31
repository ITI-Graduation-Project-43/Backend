using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _context;

        public ArticleService(IArticleRepository context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync(params Expression<Func<Article, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Article> GetByIdAsync(int id, params Expression<Func<Article, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }

        Task<Article> IRepository<Article, int>.AddAsync(Article entity)
        {
            return _context.AddAsync(entity);
        }

        Task IRepository<Article, int>.DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }

        Task<IEnumerable<Article>> IRepository<Article, int>.GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        Task<Article> IRepository<Article, int>.GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        Task IRepository<Article, int>.UpdateAsync(Article entity)
        {
            return _context.UpdateAsync(entity);
        }
    }
}