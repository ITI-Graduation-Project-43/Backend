using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                        .Include(lesson => lesson.Article)
                        .Include(lesson => lesson.Quiz)
                        .ThenInclude(quiz => quiz.Questions)
                        .Include(lesson => lesson.Video)
                        .Where(lesson => lesson.Id == lessonId && !lesson.IsDeleted)
                        .SingleOrDefaultAsync();
        }


        public async Task<IQueryable<Lesson>> GetByChapterIdAsync(int chapterId)
        {
            var lessons = await _context.Lessons
                       .Include(lesson => lesson.Chapter)
                       .ThenInclude(lessonChapter => lessonChapter.Course)
                       .Where(lesson => lesson.ChapterId == chapterId && !lesson.IsDeleted)
                       .ToListAsync();

            return lessons.AsQueryable();
        }

        public async Task<IQueryable<Lesson>> GetByCourseAndChapterIdAsync(int courseId, int chapterId)
        {
            var lessons = await _context.Lessons
                       .Include(lesson => lesson.Chapter)
                       .ThenInclude(lessonChapter => lessonChapter.Course)
                       .Where(lesson => lesson.ChapterId == chapterId && !lesson.IsDeleted)
                       .ToListAsync();

            return lessons.AsQueryable();
        }

        public async Task<IQueryable<Lesson>> GetByCourseIdAsync(int courseId)
        {
            var lessons = await _context.Lessons
                       .Include(lesson => lesson.Chapter)
                       .ThenInclude(lessonChapter => lessonChapter.Course)
                       .Where(lesson => lesson.Chapter.CourseId == courseId && !lesson.IsDeleted)
                       .ToListAsync();

            return lessons.AsQueryable();
        }

        public async Task<IQueryable<Lesson>> GetByTypeAsync(int courseId, LessonType type)
        {
            var lessons = await _context.Lessons
               .Include(lesson => lesson.Chapter)
               .ThenInclude(chapter => chapter.Course)
               .Where(lesson => lesson.Chapter.CourseId == courseId && lesson.Type == type && !lesson.IsDeleted)
               .ToListAsync();

            return lessons.AsQueryable();
        }

        public async Task<IQueryable<Lesson>> GetFreeByCourseIdAsync(int courseId)
        {
            var lessons = await _context.Lessons
               .Include(lesson => lesson.Chapter)
               .ThenInclude(chapter => chapter.Course)
               .Where(lesson => lesson.Chapter.CourseId == courseId && lesson.IsFree && !lesson.IsDeleted)
               .ToListAsync();

            return lessons.AsQueryable();
        }

        public async Task<Lesson> AddArticleLessonAsync(Lesson lesson)
        {

            _context.Lessons.Add(lesson);
            if (lesson.Article != null)
            {
                _context.Entry(lesson.Article).State = EntityState.Added;
            }
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> AddVideoLessonAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            if (lesson.Video != null)
            {
                _context.Entry(lesson.Video).State = EntityState.Added;
            }
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> AddQuizLessonAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            if (lesson.Quiz != null)
            {
                _context.Entry(lesson.Quiz).State = EntityState.Added;

                if (lesson.Quiz.Questions != null)
                {
                    foreach (var question in lesson.Quiz.Questions)
                    {
                        _context.Entry(question).State = EntityState.Added;
                    }
                }
            }
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> UpdateArticleLessonAsync(int id, Lesson lesson)
        {
            var lessonInDb = await _context.Lessons
                                            .Include(l => l.Article)
                                            .SingleOrDefaultAsync(l => l.Id == id)
                                            ?? throw new Exception(string.Format(ErrorMessages.ResourceNotFound, $"Lesson with id {id}"));

            _context.Entry(lessonInDb).CurrentValues.SetValues(lesson);

            if (lessonInDb.Article != null)
            {
                _context.Entry(lessonInDb.Article).State = EntityState.Deleted;
                lessonInDb.Article = null;
            }

            if (lesson.Article != null)
            {
                lessonInDb.Article = new Article
                {
                    Content = lesson.Article.Content
                };
            }

            _context.Lessons.Update(lessonInDb);
            await _context.SaveChangesAsync();

            return lessonInDb;
        }

        public async Task<Lesson> UpdateVideoLessonAsync(int id, Lesson lesson)
        {
            var lessonInDb = await _context.Lessons
                                           .Include(l => l.Video)
                                           .SingleOrDefaultAsync(l => l.Id == id) ?? throw new Exception(string.Format(ErrorMessages.ResourceNotFound, $"Lesson with id {id}"));

            _context.Entry(lessonInDb).CurrentValues.SetValues(lesson);

            if (lessonInDb.Video != null)
            {
                _context.Entry(lessonInDb.Video).State = EntityState.Deleted;
                lessonInDb.Video = null;
            }
            if (lesson.Video != null)
            {
                lesson.Video = new Video
                {
                    VideoUrl = lesson.Video.VideoUrl
                };
            }


            _context.Lessons.Update(lessonInDb);
            await _context.SaveChangesAsync();

            return lessonInDb;
        }

        public async Task<Lesson> UpdateQuizLessonAsync(int id, Lesson lesson)
        {
            var lessonInDb = await _context.Lessons
                                           .Include(l => l.Quiz)
                                           .ThenInclude(q => q.Questions)
                                           .SingleOrDefaultAsync(l => l.Id == id) ?? throw new Exception(string.Format(ErrorMessages.ResourceNotFound, $"Lesson with id {id}"));

            _context.Entry(lessonInDb).CurrentValues.SetValues(lesson);

            if (lessonInDb.Quiz != null)
            {
                if (lessonInDb.Quiz.Questions != null)
                {
                    foreach (var question in lessonInDb.Quiz.Questions.ToList())
                    {
                        _context.Entry(question).State = EntityState.Deleted;
                    }
                    lessonInDb.Quiz.Questions.Clear();
                }
                _context.Entry(lessonInDb.Quiz).State = EntityState.Deleted;
                lessonInDb.Quiz = null;
            }
            if (lesson.Quiz != null)
            {
                lessonInDb.Quiz = lesson.Quiz;
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