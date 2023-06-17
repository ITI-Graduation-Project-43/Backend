using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using Serilog;
using System;
using System.Collections.Generic;
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

        public async Task<IQueryable<CourseFeedback>> GetFeedbackByCourseId(int CourseId)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Course).Where(e => e.CourseId == CourseId && !e.IsDeleted);
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<IQueryable<CourseFeedback>> GetFeedbackByInstructorId(string InstructorId)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Course).Include(e => e.Instructor).Where(e => e.InstructorId == InstructorId && !e.IsDeleted);
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<IQueryable<CourseFeedback>> GetFeedbackByCourseIdAndInstructorId(int CourseId, string InstructorId)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Instructor).Include(e => e.Course).Where(e => e.CourseId == CourseId && e.InstructorId == InstructorId && !e.IsDeleted);
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<IQueryable<CourseFeedback>> GetTopCoursesRating(int NumberOfCourses)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Course).Where(r => !r.IsDeleted).OrderByDescending(e => e.CourseRating).Take(NumberOfCourses);
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<IQueryable<CourseFeedback>> GetTopInstructorsRating(int NumberOfInstructors)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Instructor).Where(r => !r.IsDeleted).OrderByDescending(e => e.InstructorRating).Take(NumberOfInstructors);
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<CourseFeedback> AddCourseFeedback(CourseFeedback CourseFeedback)
        {
            _context.CourseFeedbacks.Add(CourseFeedback);
            await _context.SaveChangesAsync();
            return CourseFeedback;
        }

        public async Task<CourseFeedback?> UpdateCourseFeedback(int Id, CourseFeedback CourseFeedback)
        {
            var CourseFeedbacks = _context.CourseFeedbacks.FirstOrDefault(e => e.Id == Id);
            if (CourseFeedbacks != null)
            {
                _context.CourseFeedbacks.Update(CourseFeedback);
                await _context.SaveChangesAsync();
                return CourseFeedback;
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
    }
}
