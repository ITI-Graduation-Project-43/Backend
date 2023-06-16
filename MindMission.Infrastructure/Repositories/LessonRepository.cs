using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Constants;
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

        public async Task<Lesson> AddArticleLessonAsync(Lesson lesson)
        {

            _context.Lessons.Add(lesson);
            if (lesson.Articles != null)
            {
                foreach (var article in lesson.Articles)
                {
                    _context.Entry(article).State = EntityState.Added;
                }
            }
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> AddVideoLessonAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            if (lesson.Videos != null)
            {
                foreach (var video in lesson.Videos)
                {
                    _context.Entry(video).State = EntityState.Added;
                }
            }
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> AddQuizLessonAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            if (lesson.Quizzes != null)
            {
                foreach (var quiz in lesson.Quizzes)
                {
                    _context.Entry(quiz).State = EntityState.Added;

                    if (quiz.Questions != null)
                    {
                        foreach (var question in quiz.Questions)
                        {
                            _context.Entry(question).State = EntityState.Added;
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> UpdateArticleLessonAsync(int id, Lesson lesson)
        {
            var lessonInDb = await _context.Lessons
                                           .Include(l => l.Articles)
                                           .SingleOrDefaultAsync(l => l.Id == id) ?? throw new Exception(string.Format(ErrorMessages.ResourceNotFound, $"Lesson with id {id}"));

            _context.Entry(lessonInDb).CurrentValues.SetValues(lesson);

            if (lessonInDb.Articles != null)
            {
                foreach (var article in lessonInDb.Articles.ToList())
                {
                    _context.Entry(article).State = EntityState.Deleted;
                }
                lessonInDb.Articles.Clear();
            }
            if (lesson.Articles != null)
            {
                foreach (var article in lesson.Articles)
                {
                    lessonInDb.Articles?.Add(article);
                }
            }

            _context.Lessons.Update(lessonInDb);
            await _context.SaveChangesAsync();

            return lessonInDb;
        }
        public async Task<Lesson> UpdateVideoLessonAsync(int id, Lesson lesson)
        {
            var lessonInDb = await _context.Lessons
                                           .Include(l => l.Videos)
                                           .SingleOrDefaultAsync(l => l.Id == id) ?? throw new Exception(string.Format(ErrorMessages.ResourceNotFound, $"Lesson with id {id}"));

            _context.Entry(lessonInDb).CurrentValues.SetValues(lesson);

            if (lessonInDb.Videos != null)
            {
                foreach (var video in lessonInDb.Videos.ToList())
                {
                    _context.Entry(video).State = EntityState.Deleted;
                }
                lessonInDb.Videos.Clear();
            }
            if (lesson.Videos != null)
            {
                foreach (var video in lesson.Videos)
                {
                    lessonInDb.Videos?.Add(video);
                }
            }

            _context.Lessons.Update(lessonInDb);
            await _context.SaveChangesAsync();

            return lessonInDb;
        }

        public async Task<Lesson> UpdateQuizLessonAsync(int id, Lesson lesson)
        {
            var lessonInDb = await _context.Lessons
                                           .Include(l => l.Quizzes)
                                           .ThenInclude(q => q.Questions)
                                           .SingleOrDefaultAsync(l => l.Id == id) ?? throw new Exception(string.Format(ErrorMessages.ResourceNotFound, $"Lesson with id {id}"));

            _context.Entry(lessonInDb).CurrentValues.SetValues(lesson);

            if (lessonInDb.Quizzes != null)
            {
                foreach (var quiz in lessonInDb.Quizzes.ToList())
                {
                    if (quiz.Questions != null)
                    {
                        foreach (var question in quiz.Questions.ToList())
                        {
                            _context.Entry(question).State = EntityState.Deleted;
                        }
                        quiz.Questions.Clear();
                    }
                    _context.Entry(quiz).State = EntityState.Deleted;
                }
                lessonInDb.Quizzes.Clear();
            }
            if (lesson.Quizzes != null)
            {
                foreach (var quiz in lesson.Quizzes)
                {
                    lessonInDb.Quizzes?.Add(quiz);
                }
            }

            _context.Lessons.Update(lessonInDb);
            await _context.SaveChangesAsync();

            return lessonInDb;
        }
        public async Task<Lesson> UpdateLessonPartialAsync(int id, Lesson lesson)
        {

            _context.Entry(lesson).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return lesson;
        }

    }
}