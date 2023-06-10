using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Infrastructure.Repositories
{
    public class CourseFeedbackRepository : ICourseFeedbackRepository
    {
        private readonly MindMissionDbContext Context;

        public CourseFeedbackRepository(MindMissionDbContext _Context)
        {
            Context = _Context;
        }

        public async Task<IQueryable<CourseFeedback>> GetFeedbackByCourseId(int CourseId)
        {
            var CourseFeedbacks = await Context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Course).Where(e => e.CourseId == CourseId).ToListAsync();
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<IQueryable<CourseFeedback>> GetFeedbackByInstructorId(string InstructorId)
        {
            var CourseFeedbacks = await Context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Instructor).Where(e => e.InstructorId == InstructorId).ToListAsync();
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<IQueryable<CourseFeedback>> GetFeedbackByCourseIdAndInstructorId(int CourseId, string InstructorId)
        {
            var CourseFeedbacks = await Context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Instructor).Include(e => e.Course).Where(e => e.CourseId == CourseId && e.InstructorId == InstructorId).ToListAsync();
            return CourseFeedbacks.AsQueryable();
        }
       
        public async Task<IQueryable<CourseFeedback>> GetTopCoursesRating(int NumberOfCourses)
        {
            var CourseFeedbacks = await Context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Course).OrderByDescending(e => e.CourseRating).Take(NumberOfCourses).ToListAsync();
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<IQueryable<CourseFeedback>> GetTopInstructorsRating(int NumberOfInstructors)
        {
            var CourseFeedbacks = await Context.CourseFeedbacks.Include(e => e.Student).Include(e => e.Instructor).OrderByDescending(e => e.InstructorRating).Take(NumberOfInstructors).ToListAsync();
            return CourseFeedbacks.AsQueryable();
        }

        public async Task<CourseFeedback> AddCourseFeedback(CourseFeedback CourseFeedback)
        {
            Context.CourseFeedbacks.Add(CourseFeedback);
            await Context.SaveChangesAsync();
            return CourseFeedback;
        }

        public async Task<CourseFeedback?> UpdateCourseFeedback(int Id, CourseFeedback CourseFeedback)
        {
            var CourseFeedbacks = Context.CourseFeedbacks.FirstOrDefault(e => e.Id == Id);
            if (CourseFeedbacks != null) 
            { 
                Context.CourseFeedbacks.Update(CourseFeedback);
                await Context.SaveChangesAsync();
                return CourseFeedback;
            }
            return null;
        }
    }
}
