using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ILessonRepository : IRepository<Lesson, int>
    {
        Task<Lesson> GetByLessonIdAsync(int lessonId);

        IQueryable<Lesson> GetByCourseIdAsync(int courseId, int pageNumber, int pageSize);
        IQueryable<Lesson> GetByChapterIdAsync(int chapterId, int pageNumber, int pageSize);

        IQueryable<Lesson> GetByCourseAndChapterIdAsync(int courseId, int chapterId, int pageNumber, int pageSize);
        IQueryable<Lesson> GetFreeByCourseIdAsync(int courseId, int pageNumber, int pageSize);
        IQueryable<Lesson> GetByTypeAsync(int courseId, LessonType type, int pageNumber, int pageSize);

        Task<Lesson> AddArticleLessonAsync(Lesson lesson);


        Task<Lesson> AddVideoLessonAsync(Lesson lesson);


        Task<Lesson> AddQuizLessonAsync(Lesson lesson);


        Task<Lesson> UpdateArticleLessonAsync(int id, Lesson lesson);

        Task<Lesson> UpdateVideoLessonAsync(int id, Lesson lesson);

        Task<Lesson> UpdateQuizLessonAsync(int id, Lesson lesson);

        Task<Lesson> UpdateLessonPartialAsync(int id, Lesson lesson);



    }
}