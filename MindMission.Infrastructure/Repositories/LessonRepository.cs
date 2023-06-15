using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class LessonRepository : Repository<Lesson, int>, ILessonRepository
    {
        private readonly MindMissionDbContext _context;

        public LessonRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Lesson> GetByLessonIdAsync(int lessonId)
        {
            return await _context.Lessons
                        .Include(lesson => lesson.Chapter)
                        .ThenInclude(chapter => chapter.Course)
                        .Include(lesson => lesson.Articles)
                        .Include(lesson => lesson.Quizzes)
                        .ThenInclude(quiz => quiz.Questions)
                        .Include(lesson => lesson.Videos)
                        .SingleOrDefaultAsync(lesson => lesson.Id == lessonId);
        }


        public async Task<IQueryable<Lesson>> GetByChapterIdAsync(int chapterId)
        {
            var lessons = await _context.Lessons
                       .Include(lesson => lesson.Chapter)
                       .ThenInclude(lessonChapter => lessonChapter.Course)
                       .Where(lesson => lesson.ChapterId == chapterId)
                       .ToListAsync();

            return lessons.AsQueryable();
        }

        public async Task<IQueryable<Lesson>> GetByCourseAndChapterIdAsync(int courseId, int chapterId)
        {
            var lessons = await _context.Lessons
                       .Include(lesson => lesson.Chapter)
                       .ThenInclude(lessonChapter => lessonChapter.Course)
                       .Where(lesson => lesson.ChapterId == chapterId)
                       .ToListAsync();

            return lessons.AsQueryable();
        }

        public async Task<IQueryable<Lesson>> GetByCourseIdAsync(int courseId)
        {
            var lessons = await _context.Lessons
                       .Include(lesson => lesson.Chapter)
                       .ThenInclude(lessonChapter => lessonChapter.Course)
                       .Where(lesson => lesson.Chapter.CourseId == courseId)
                       .ToListAsync();

            return lessons.AsQueryable();
        }

        public async Task<IQueryable<Lesson>> GetByTypeAsync(int courseId, LessonType type)
        {
            var lessons = await _context.Lessons
               .Include(lesson => lesson.Chapter)
               .ThenInclude(chapter => chapter.Course)
               .Where(lesson => lesson.Chapter.CourseId == courseId && lesson.Type == type)
               .ToListAsync();

            return lessons.AsQueryable();
        }

        public async Task<IQueryable<Lesson>> GetFreeByCourseIdAsync(int courseId)
        {
            var lessons = await _context.Lessons
               .Include(lesson => lesson.Chapter)
               .ThenInclude(chapter => chapter.Course)
               .Where(lesson => lesson.Chapter.CourseId == courseId && lesson.IsFree)
               .ToListAsync();

            return lessons.AsQueryable();
        }
    }
}