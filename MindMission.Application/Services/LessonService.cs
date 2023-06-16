using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services_Classes
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _context;

        public LessonService(ILessonRepository context)
        {
            _context = context;
        }
        #region Get
        public Task<IQueryable<Lesson>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public async Task<IEnumerable<Lesson>> GetAllAsync(params Expression<Func<Lesson, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Lesson> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Lesson> GetByIdAsync(int id, params Expression<Func<Lesson, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }

        public Task<IQueryable<Lesson>> GetByCourseIdAsync(int courseId)
        {
            return _context.GetByCourseIdAsync(courseId);
        }

        public Task<IQueryable<Lesson>> GetByChapterIdAsync(int chapterId)
        {
            return _context.GetByChapterIdAsync(chapterId);
        }

        public Task<IQueryable<Lesson>> GetByCourseAndChapterIdAsync(int courseId, int chapterId)
        {
            return _context.GetByCourseAndChapterIdAsync(courseId, chapterId);
        }

        public Task<IQueryable<Lesson>> GetFreeByCourseIdAsync(int courseId)
        {
            return _context.GetFreeByCourseIdAsync(courseId);
        }

        public Task<IQueryable<Lesson>> GetByTypeAsync(int courseId, LessonType type)
        {
            return _context.GetByTypeAsync(courseId, type);
        }

        public Task<Lesson> GetByLessonIdAsync(int lessonId)
        {
            return _context.GetByLessonIdAsync(lessonId);
        }
        #endregion
        public Task<Lesson> AddAsync(Lesson entity)
        {
            return _context.AddAsync(entity);
        }

        public Task<Lesson> UpdateAsync(Lesson entity)
        {
            return _context.UpdateAsync(entity);
        }
        public async Task<Lesson> UpdatePartialAsync(int id, Lesson entity)
        {
            return await _context.UpdatePartialAsync(id, entity);
        }
        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }

        public Task SoftDeleteAsync(int id)
        {
            return _context.SoftDeleteAsync(id);
        }

        public async Task<Lesson> AddArticleLessonAsync(Lesson lesson)
        {
            return await _context.AddArticleLessonAsync(lesson);
        }
        public async Task<Lesson> AddVideoLessonAsync(Lesson lesson)
        {
            return await _context.AddVideoLessonAsync(lesson);
        }
        public async Task<Lesson> AddQuizLessonAsync(Lesson lesson)
        {
            return await _context.AddQuizLessonAsync(lesson);
        }
        public async Task<Lesson> UpdateArticleLessonAsync(int id, Lesson lesson)
        {
            return await _context.UpdateArticleLessonAsync(id, lesson);
        }

        public async Task<Lesson> UpdateVideoLessonAsync(int id, Lesson lesson)
        {
            return await _context.UpdateVideoLessonAsync(id, lesson);
        }

        public async Task<Lesson> UpdateQuizLessonAsync(int id, Lesson lesson)
        {
            return await _context.UpdateQuizLessonAsync(id, lesson);
        }
        public async Task<Lesson> UpdateLessonPartialAsync(int id, Lesson lesson)
        {
            return await _context.UpdateLessonPartialAsync(id, lesson);
        }

    }
}