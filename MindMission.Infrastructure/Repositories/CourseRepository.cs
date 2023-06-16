﻿using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Constants;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using System.Linq;

namespace MindMission.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        private readonly MindMissionDbContext _context;

        public CourseRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Course> GetByNameAsync(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var entity = await _context.Courses
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                            .FirstOrDefaultAsync(c => c.Title.ToLower() == name.ToLower());

            return entity ?? throw new KeyNotFoundException($"No entity with name {name} found.");

        }

        public async Task<IQueryable<Course>> GetAllByCategoryAsync(int categoryId)
        {

            var courses = await _context.Courses
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                         .Where(c => c.CategoryId == categoryId ||
                                     c.Category.ParentId == categoryId ||
                                     c.Category.Parent.ParentId == categoryId)
                         .ToListAsync();

            return courses.AsQueryable();
        }

        public async Task<IQueryable<Course>> GetRelatedCoursesAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId) ?? throw new Exception($"Course with id {courseId} not found.");
            int topicId = course.CategoryId;
            var Subcategory = await _context.Categories
                                          .Include(c => c.Parent)
                                          .ThenInclude(c => c.Parent)
                                          .FirstOrDefaultAsync(c => c.Id == topicId) ?? throw new Exception($"SubCategory with id {topicId} not found.");
            int SubcategoryId = (int)Subcategory.ParentId;
            int categoryId = (int)Subcategory.Parent.ParentId;

            var relatedCourses = _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Chapters)
                .Include(c => c.CourseRequirements)
                .Include(c => c.LearningItems)
                .Include(c => c.EnrollmentItems)
                .Include(c => c.Category)
                    .ThenInclude(c => c.Parent)
                        .ThenInclude(c => c.Parent)
                .Where(c => (c.CategoryId == topicId ||
                            c.Category.ParentId == SubcategoryId ||
                            c.Category.Parent.ParentId == categoryId) &&
                            c.Id != courseId);

            return relatedCourses.AsQueryable();
        }

        public async Task<IQueryable<Course>> GetAllByInstructorAsync(string instructorId)
        {
            var courses = await _context.Courses
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                         .Where(c => c.InstructorId == instructorId)
                         .ToListAsync();

            return courses.AsQueryable();
        }

        public async Task<IQueryable<Course>> GetInstructorOtherCourses(string instructorId, int courseId)
        {
            var courses = await _context.Courses
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                         .Where(c => c.InstructorId == instructorId && c.Id != courseId)
                         .ToListAsync();

            return courses.AsQueryable();
        }

        public async Task<IQueryable<Course>> GetTopRatedCoursesAsync(int topNumber)
        {
            var courses = await _context.Courses
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                            .OrderByDescending(c => c.AvgReview)
                            .Take(topNumber)
                            .ToListAsync();

            return courses.AsQueryable();
        }

        public async Task<IQueryable<Course>> GetRecentCoursesAsync(int recentNumber)
        {
            var courses = await _context.Courses
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .Include(c => c.Category)
                         .OrderByDescending(c => c.CreatedAt)
                         .Take(recentNumber)
                         .ToListAsync();

            return courses.AsQueryable();
        }

        public async Task<StudentCourseDto> GetCourseByIdWithStudentsAsync(int courseId, int studentsNumber)
        {
            var course = await _context.Courses
                        .Include(c => c.Instructor)
                        .Include(c => c.Enrollments)
                            .ThenInclude(e => e.Student)
                        .Where(c => c.Id == courseId)
                        .Select(c => new StudentCourseDto
                        {
                            CourseId = c.Id,
                            Price = c.Price,
                            CourseTitle = c.Title,
                            CourseImageUrl = c.ImageUrl,
                            CourseAvgReview = c.AvgReview,
                            CourseNoOfStudents = c.NoOfStudents,
                            CourseDiscount = c.Discount,
                            CourseCategoryName = c.Category.Name,
                            InstructorId = c.Instructor.Id,
                            InstructorFirstName = c.Instructor.FirstName,
                            InstructorLastName = c.Instructor.LastName,
                            InstructorProfilePicture = c.Instructor.ProfilePicture,
                            Student = c.Enrollments.Select(e => new CustomStudentDto
                            {
                                StudentId = e.Student.Id,
                                StudentFirstName = e.Student.FirstName,
                                StudentLastName = e.Student.LastName,
                                StudentProfilePicture = e.Student.ProfilePicture
                            }).Take(studentsNumber).ToList()
                        })
                        .FirstOrDefaultAsync();

            return course ?? throw new NullReferenceException("Course");
        }

        public async Task<IQueryable<StudentCourseDto>> GetRelatedCoursesWithStudentsAsync(int courseId, int studentsNumber)
        {
            var course = await _context.Courses.FindAsync(courseId) ?? throw new Exception($"Course with id {courseId} not found.");
            int topicId = course.CategoryId;
            var subcategory = await _context.Categories
                .Include(c => c.Parent)
                    .ThenInclude(c => c.Parent)
                .FirstOrDefaultAsync(c => c.Id == topicId) ?? throw new Exception($"SubCategory with id {topicId} not found.");
            int subcategoryId = (int)subcategory.ParentId;
            int categoryId = (int)subcategory.Parent.ParentId;

            var relatedCourses = await _context.Courses
                                .Include(c => c.Instructor)
                                .Include(c => c.Enrollments)
                                    .ThenInclude(e => e.Student)
                                .Where(c => (c.CategoryId == topicId ||
                                            c.Category.ParentId == subcategoryId ||
                                            c.Category.Parent.ParentId == categoryId) &&
                                            c.Id != courseId)
                                .Select(c => new StudentCourseDto
                                {
                                    CourseId = c.Id,
                                    Price = c.Price,
                                    CourseTitle = c.Title,
                                    CourseImageUrl = c.ImageUrl,
                                    CourseAvgReview = c.AvgReview,
                                    CourseNoOfStudents = c.NoOfStudents,
                                    CourseDiscount = c.Discount,
                                    CourseCategoryName = c.Category.Name,
                                    InstructorId = c.Instructor.Id,
                                    InstructorFirstName = c.Instructor.FirstName,
                                    InstructorLastName = c.Instructor.LastName,
                                    InstructorProfilePicture = c.Instructor.ProfilePicture,
                                    Student = c.Enrollments
                                        .Where(e => e.Student.ProfilePicture != null)
                                        .Select(e => new CustomStudentDto
                                        {
                                            StudentId = e.Student.Id,
                                            StudentFirstName = e.Student.FirstName,
                                            StudentLastName = e.Student.LastName,
                                            StudentProfilePicture = e.Student.ProfilePicture
                                        })
                                        .Take(studentsNumber)
                                        .ToList()
                                }).ToListAsync();

            return relatedCourses.AsQueryable();
        }

        public async Task<IQueryable<StudentCourseDto>> GetInstructorOtherWithStudentsCourses(string instructorId, int courseId, int studentsNumber)
        {
            var instructorCourses = await _context.Courses
                    .Include(c => c.Instructor)
                    .Include(c => c.Enrollments)
                        .ThenInclude(e => e.Student)
                    .Where(c => c.InstructorId == instructorId && c.Id != courseId)
                    .Select(c => new StudentCourseDto
                    {
                        CourseId = c.Id,
                        Price = c.Price,
                        CourseTitle = c.Title,
                        CourseImageUrl = c.ImageUrl,
                        CourseAvgReview = c.AvgReview,
                        CourseNoOfStudents = c.NoOfStudents,
                        CourseDiscount = c.Discount,
                        CourseCategoryName = c.Category.Name,
                        InstructorId = c.Instructor.Id,
                        InstructorFirstName = c.Instructor.FirstName,
                        InstructorLastName = c.Instructor.LastName,
                        InstructorProfilePicture = c.Instructor.ProfilePicture,
                        Student = c.Enrollments
                            .Where(e => e.Student.ProfilePicture != null)
                            .Select(e => new CustomStudentDto
                            {
                                StudentId = e.Student.Id,
                                StudentFirstName = e.Student.FirstName,
                                StudentLastName = e.Student.LastName,
                                StudentProfilePicture = e.Student.ProfilePicture
                            })
                            .Take(studentsNumber)
                            .ToList()
                    })
                    .ToListAsync();

            return instructorCourses.AsQueryable();
        }

        public async Task<Course> GetFeatureThisWeekCourse()
        {
            DateTime CutoffDate = DateTime.Now.AddDays(-7);

            return await _context.Courses.Include(c => c.Instructor).Include(c => c.Enrollments.Where(en => en.EnrollmentDate >= CutoffDate))
                .OrderByDescending(c => c.Enrollments.Count())
                .FirstOrDefaultAsync() ?? throw new Exception($"Feature This Week Course not found.");
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);

            foreach (var learningItem in course.LearningItems)
            {
                _context.Entry(learningItem).State = EntityState.Added;
            }
            foreach (var enrollmentItem in course.EnrollmentItems)
            {
                _context.Entry(enrollmentItem).State = EntityState.Added;
            }
            if (course.CourseRequirements != null)
            {
                foreach (var requirement in course.CourseRequirements)
                {
                    _context.Entry(requirement).State = EntityState.Added;
                }
            }

            await _context.SaveChangesAsync();

            return course;
        }

        public async Task<Course> UpdateCourseAsync(int id, Course course)
        {
            var courseInDb = await _context.Courses
                                        .Include(c => c.LearningItems)
                                        .Include(c => c.EnrollmentItems)
                                        .Include(c => c.CourseRequirements)
                                        .SingleOrDefaultAsync(c => c.Id == id) ?? throw new Exception(string.Format(ErrorMessages.ResourceNotFound, $"Course with id {id}"));

            _context.Entry(courseInDb).CurrentValues.SetValues(course);

            foreach (var learningItem in courseInDb.LearningItems.ToList())
            {
                _context.Entry(learningItem).State = EntityState.Deleted;
            }
            courseInDb.LearningItems.Clear();
            foreach (var learningItem in course.LearningItems)
            {
                courseInDb.LearningItems.Add(learningItem);
            }

            foreach (var enrollmentItem in courseInDb.EnrollmentItems.ToList())
            {
                _context.Entry(enrollmentItem).State = EntityState.Deleted;
            }
            courseInDb.EnrollmentItems.Clear();
            foreach (var enrollmentItem in course.EnrollmentItems)
            {
                courseInDb.EnrollmentItems.Add(enrollmentItem);
            }

            if (courseInDb.CourseRequirements != null)
            {
                foreach (var requirement in courseInDb.CourseRequirements.ToList())
                {
                    _context.Entry(requirement).State = EntityState.Deleted;
                }
                courseInDb.CourseRequirements.Clear();
            }
            if (course.CourseRequirements != null)
            {
                foreach (var requirement in course.CourseRequirements)
                {
                    courseInDb.CourseRequirements?.Add(requirement);
                }
            }

            _context.Courses.Update(courseInDb);
            await _context.SaveChangesAsync();

            return courseInDb;
        }

        public async Task<Course> UpdateCoursePartialAsync(int id, Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return course;
        }
    }
}