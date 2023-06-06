using MindMission.Application.DTOs;
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

        public Task<Lesson> AddAsync(Lesson entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Lesson entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
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
    }
}