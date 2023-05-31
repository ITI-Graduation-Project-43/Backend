using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IArticleRepository : IRepository<Article, int>
    {
    }
}