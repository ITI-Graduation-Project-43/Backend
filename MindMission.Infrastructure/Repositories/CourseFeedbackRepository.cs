using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MindMission.Infrastructure.Repositories
{
    public class CourseFeedbackRepository : ICourseFeedbackRepository
    {
        private readonly MindMissionDbContext _context;

        public CourseFeedbackRepository(MindMissionDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetTotalCountAsync()
        {
            return await _context.CourseFeedbacks.CountAsync(x => !x.IsDeleted);
        }
        public IQueryable<CourseFeedback> GetFeedbackByCourseId(int courseId, int pageNumber, int pageSize)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.AsSplitQuery().Include(e => e.Student).Include(e => e.Course).Where(e => e.CourseId == courseId && !e.IsDeleted)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return CourseFeedbacks.AsQueryable();
        }

        public IQueryable<CourseFeedback> GetFeedbackByInstructorId(string instructorId, int pageNumber, int pageSize)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.AsSplitQuery().Include(e => e.Student).Include(e => e.Course).Include(e => e.Instructor).Where(e => e.InstructorId == instructorId && !e.IsDeleted)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize);

            return CourseFeedbacks.AsQueryable();
        }

        public IQueryable<CourseFeedback> GetFeedbackByCourseIdAndInstructorId(int courseId, string instructorId, int pageNumber, int pageSize)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.AsSplitQuery().Include(e => e.Student).Include(e => e.Instructor).Include(e => e.Course).Where(e => e.CourseId == courseId && e.InstructorId == instructorId && !e.IsDeleted)
                                  .Skip((pageNumber - 1) * pageSize)
                                                        .Take(pageSize);
            return CourseFeedbacks.AsQueryable();
        }

        public IQueryable<CourseFeedback> GetTopCoursesRating(int numberOfCourses, int pageNumber, int pageSize)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.AsSplitQuery().Include(e => e.Student).Include(e => e.Course).Where(r => !r.IsDeleted).OrderByDescending(e => e.CourseRating).Take(numberOfCourses)
                                  .Skip((pageNumber - 1) * pageSize)
                                                        .Take(pageSize);
            return CourseFeedbacks.AsQueryable();
        }

        public IQueryable<CourseFeedback> GetTopInstructorsRating(int numberOfInstructor, int pageNumber, int pageSize)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.AsSplitQuery().Include(e => e.Student).Include(e => e.Instructor).Where(r => !r.IsDeleted).OrderByDescending(e => e.InstructorRating).Take(numberOfInstructor)
                                  .Skip((pageNumber - 1) * pageSize)
                                                        .Take(pageSize);
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<CourseFeedback> AddCourseFeedback(CourseFeedback courseFeedback)
        {
            _context.CourseFeedbacks.Add(courseFeedback);
            await _context.SaveChangesAsync();
            return courseFeedback;
        }

        public async Task<CourseFeedback?> UpdateCourseFeedback(int Id, CourseFeedback courseFeedback)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.FirstOrDefault(e => e.Id == Id);
            if (CourseFeedbacks != null)
            {
                _context.CourseFeedbacks.Update(courseFeedback);
                await _context.SaveChangesAsync();
                return courseFeedback;
            }
            return null;
        }
        public async Task SoftDeleteAsync(int id)
        {
            var entity = await _context.CourseFeedbacks.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AddCourseFeedbackDto> GetFeedbackByCourseIdAndStudentId(int courseId, string studentId)
        {
            return await _context.CourseFeedbacks
                 .Where(w => w.StudentId == studentId && w.CourseId == courseId && !w.IsDeleted)
                 .Select(e => new AddCourseFeedbackDto
                 {
                     Id = e.Id,
                     CourseId = e.Course.Id,
                     StudentId = e.Student.Id,
                     InstructorId = e.InstructorId,
                     InstructorRating = e.InstructorRating,
                     CourseRating = e.CourseRating,
                     FeedbackText = e.FeedbackText,

                 })
                 .FirstOrDefaultAsync() ?? throw new NullReferenceException("Course Feedback not found");
        }
    }
}
