using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ILessonRepository : IRepository<Lesson, int>
    {
        Task<IQueryable<Lesson>> GetByCourseIdAsync(int courseId);
        Task<IQueryable<Lesson>> GetByChapterIdAsync(int chapterId);
        Task<IQueryable<Lesson>> GetByCourseAndChapterIdAsync(int courseId, int chapterId);
        Task<IQueryable<Lesson>> GetFreeByCourseIdAsync(int courseId);
        Task<IQueryable<Lesson>> GetByTypeAsync(int courseId, LessonType type);
    }
}