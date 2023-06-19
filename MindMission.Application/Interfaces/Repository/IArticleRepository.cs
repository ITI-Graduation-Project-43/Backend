using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IArticleRepository : IRepository<Article, int>
    {
    }
}