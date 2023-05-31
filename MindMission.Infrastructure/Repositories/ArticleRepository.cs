using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class ArticleRepository : Repository<Article, int>, IArticleRepository
    {
        private readonly MindMissionDbContext _context;

        public ArticleRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }
    }
}