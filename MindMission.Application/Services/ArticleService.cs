using MindMission.Application.Exceptions;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Constants;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class ArticleService : Service<Article, int>, IArticleService
    {

        private readonly IArticleRepository _context;
        private readonly ILessonService _lessonService;
        private const string entity = "Article";

        public ArticleService(IArticleRepository context, ILessonService lessonService) : base(context)
        {
            _lessonService = lessonService;
            _context = context;
        }

        public override async Task DeleteAsync(Article article)
        {

            if (article != null)
            {
                using var transaction = _context.Context.Database.BeginTransaction();
                try
                {
                    await base.DeleteAsync(article);
                    await _lessonService.DeleteAsync(article.LessonId);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            else
            {
                throw new KeyNotFoundException(string.Format(ErrorMessages.ResourceNotFound, entity));
            }
        }
        public override async Task SoftDeleteAsync(Article article)
        {
            if (article != null)
            {
                using var transaction = _context.Context.Database.BeginTransaction();
                try
                {
                    await _lessonService.SoftDeleteAsync(article.LessonId);
                    await base.SoftDeleteAsync(article);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            else
            {
                throw new KeyNotFoundException(string.Format(ErrorMessages.ResourceNotFound, entity));
            }
        }

    }
}