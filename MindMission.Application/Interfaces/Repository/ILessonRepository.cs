using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ILessonRepository : IRepository<Lesson, int>
    {
        public Task<IQueryable<Lesson>> GetByCourseIdAsync(int courseId);
        public Task<IQueryable<Lesson>> GetByChapterIdAsync(int chapterId);
        public Task<IQueryable<Lesson>> GetByCourseAndChapterIdAsync(int courseId, int chapterId);
        public Task<IQueryable<Lesson>> GetFreeByCourseIdAsync(int courseId);
        public Task<IQueryable<Lesson>> GetByTypeAsync(int courseId, LessonType type);
    }
}