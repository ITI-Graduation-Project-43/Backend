using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using System.Drawing.Printing;
using System.Formats.Asn1;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MindMission.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        private readonly MindMissionDbContext _context;

        public CourseRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IQueryable<Course>> GetAllAsync()
        {
            var courses = await _context.Courses.AsSplitQuery()
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Attachment)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                            .Where(c => !c.IsDeleted && c.Approved)
                            .ToListAsync();

            return courses.AsQueryable();
        }
        public override IQueryable<Course> GetAllAsync(int pageNumber, int pageSize)
        {
            var Query = _context.Courses.AsSplitQuery()
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Attachment)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                            .Where(c => !c.IsDeleted && c.Approved)
                                .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return Query.AsQueryable();
        }
        public override async Task<Course> GetByIdAsync(int id)
        {

            var entity = await _context.Courses.AsSplitQuery()
                           .Include(c => c.Instructor)
                           .Include(c => c.Chapters)
                           .ThenInclude(c => c.Lessons)
                           .ThenInclude(l => l.Attachment)
                           .Include(c => c.CourseRequirements)
                           .Include(c => c.LearningItems)
                           .Include(c => c.EnrollmentItems)
                           .Include(c => c.Category)
                           .ThenInclude(c => c.Parent)
                           .ThenInclude(c => c.Parent)
                           .Where(c => !c.IsDeleted && c.Id == id)
                           .FirstOrDefaultAsync();

            return entity ?? throw new KeyNotFoundException($"No entity with id {id} found.");
        }

        public async Task<int> GetCourseNumber()
        {
            return await _context.Courses.Where(e => e.Approved).CountAsync();
        }
        public async Task<int> GetCourseNumberByCourseId(int courseId)
        {
            return await _context.Courses
                            .CountAsync(c => c.Id == courseId && c.Approved);
        }
        public async Task<int> GetCourseRelatedNumber(int courseId)
        {
            var relatedCoursesQuery = await GetRelatedCoursesQueryAsync(courseId);
            return await relatedCoursesQuery.CountAsync();
        }
        public async Task<int> GetCourseNumberByCategoryId(int categoryId)
        {
            return await _context.Courses
                            .CountAsync(c => c.CategoryId == categoryId && c.Approved);
        }
        public async Task<int> GetCourseNumberByCourseIdAndInstructorId(int courseId, string instructorId)
        {
            return await _context.Courses
                            .CountAsync(c => c.Id == courseId && c.InstructorId == instructorId && c.Approved);
        }
        public async Task<int> GetCourseNumberByInstructorId(string instructorId)
        {
            return await _context.Courses
                            .CountAsync(c => c.InstructorId == instructorId && c.Approved);
        }

        public async Task<decimal> GetAvgRateCourses()
        {
            return _context.Courses.Average(e => e.AvgReview);
        }

        public async Task<Course> GetByNameAsync(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var entity = await _context.Courses.AsSplitQuery()
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Attachment)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                            .Where(c => c.Title.ToLower() == name.ToLower() && !c.IsDeleted)
                            .FirstOrDefaultAsync();

            return entity ?? throw new KeyNotFoundException($"No entity with name {name} found.");

        }

        public IQueryable<Course> GetAllByCategoryAsync(int categoryId, int pageNumber, int pageSize)
        {

            return _context.Courses.AsSplitQuery()
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Attachment)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                         .Where(c => c.CategoryId == categoryId ||
                                     c.Category.ParentId == categoryId ||
                                     c.Category.Parent.ParentId == categoryId && !c.IsDeleted && c.Approved)
                          .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

        }

        public async Task<IQueryable<Course>> GetRelatedCoursesAsync(int courseId, int pageNumber, int pageSize)
        {
            var course = await _context.Courses.FindAsync(courseId) ?? throw new Exception($"Course with id {courseId} not found.");
            int topicId = course.CategoryId;
            var Subcategory = await _context.Categories.AsSplitQuery()
                                          .Include(c => c.Parent)
                                          .ThenInclude(c => c.Parent)
                                          .Where(c => c.Id == topicId && !c.IsDeleted)
                                          .FirstOrDefaultAsync() ?? throw new Exception($"SubCategory with id {topicId} not found.");
            int SubcategoryId = (int)Subcategory.ParentId;
            int categoryId = (int)Subcategory.Parent.ParentId;

            var relatedCourses = _context.Courses
                .AsSplitQuery()
                .Include(c => c.Instructor)
                .Include(c => c.Chapters)
                .ThenInclude(c => c.Lessons)
                .ThenInclude(l => l.Attachment)
                .Include(c => c.CourseRequirements)
                .Include(c => c.LearningItems)
                .Include(c => c.EnrollmentItems)
                .Include(c => c.Category)
                    .ThenInclude(c => c.Parent)
                        .ThenInclude(c => c.Parent)
                .Where(c => (c.CategoryId == topicId ||
                            c.Category.ParentId == SubcategoryId ||
                            c.Category.Parent.ParentId == categoryId) &&
                            c.Id != courseId && !c.IsDeleted && c.Approved).Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return relatedCourses.AsQueryable();
        }
        public IQueryable<Course> GetAllByInstructorAsync(string instructorId, int pageNumber, int pageSize)
        {
            var courses = _context.Courses
                            .AsSplitQuery()
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Attachment)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                         .Where(c => c.InstructorId == instructorId && !c.IsDeleted && c.Approved)
                         .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return courses.AsQueryable();
        }
        public async Task<IQueryable<Course>> GetAllByInstructorAsync(string instructorId)
        {
            var courses = await _context.Courses
                            .AsSplitQuery()
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Attachment)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                         .Where(c => c.InstructorId == instructorId && !c.IsDeleted && c.Approved)
                         .ToListAsync();

            return courses.AsQueryable();
        }

        public IQueryable<Course> GetInstructorOtherCourses(string instructorId, int courseId, int pageNumber, int pageSize)
        {
            var courses = _context.Courses
                            .AsSplitQuery()
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Attachment)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                         .Where(c => c.InstructorId == instructorId && c.Id != courseId && !c.IsDeleted && c.Approved)
                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return courses.AsQueryable();
        }

        public IQueryable<Course> GetTopRatedCoursesAsync(int topNumber, int pageNumber, int pageSize)
        {
            var courses = _context.Courses
                            .AsSplitQuery()
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Attachment)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                            .Where(c => !c.IsDeleted && c.Approved)
                            .OrderByDescending(c => c.AvgReview)
                            .Take(topNumber).Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return courses.AsQueryable();
        }

        public IQueryable<Course> GetRecentCoursesAsync(int recentNumber, int pageNumber, int pageSize)
        {
            var courses = _context.Courses.AsSplitQuery()
                         .Include(c => c.Instructor)
                         .Include(c => c.Chapters)
                         .ThenInclude(c => c.Lessons)
                         .ThenInclude(l => l.Attachment)
                         .Include(c => c.Category)
                         .Where(c => !c.IsDeleted && c.Approved)
                         .OrderByDescending(c => c.CreatedAt)
                         .Take(recentNumber)
                         .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return courses.AsQueryable();
        }

        public async Task<StudentCourseDto> GetCourseByIdWithStudentsAsync(int courseId, int studentsNumber)
        {
            var course = await _context.Courses
                        .AsSplitQuery()
                        .Include(c => c.Instructor)
                        .Include(c => c.Enrollments)
                            .ThenInclude(e => e.Student)
                        .Where(c => c.Id == courseId && !c.IsDeleted && c.Approved)
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

        public async Task<IQueryable<StudentCourseDto>> GetRelatedCoursesWithStudentsAsync(int courseId, int studentsNumber, int pageNumber, int pageSize)
        {
            var relatedCoursesQuery = await GetRelatedCoursesQueryAsync(courseId);
            var relatedCourses = relatedCoursesQuery.AsSplitQuery()
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                                        .Select(c => new StudentCourseDto
                                        {
                                            CourseId = c.Id,
                                            Price = c.Price,
                                            CourseTitle = c.Title,
                                            CourseDescription = c.ShortDescription,
                                            CourseImageUrl = c.ImageUrl,
                                            CourseAvgReview = c.AvgReview,
                                            CourseNoOfStudents = c.NoOfStudents,
                                            CourseDiscount = c.Discount,
                                            CourseCategoryName = c.Category.Name,
                                            InstructorId = c.Instructor.Id,
                                            InstructorFirstName = c.Instructor.FirstName,
                                            InstructorLastName = c.Instructor.LastName,
                                            InstructorProfilePicture = c.Instructor.ProfilePicture ?? " ",
                                            Student = c.Enrollments
                                        .Where(e => e.Student.ProfilePicture != null && !e.IsDeleted)
                                        .Select(e => new CustomStudentDto
                                        {
                                            StudentId = e.Student.Id,
                                            StudentFirstName = e.Student.FirstName,
                                            StudentLastName = e.Student.LastName,
                                            StudentProfilePicture = e.Student.ProfilePicture
                                        })
                                        .Take(studentsNumber)
                                        .ToList()
                                        }).Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return relatedCourses.AsQueryable();
        }

        public IQueryable<StudentCourseDto> GetInstructorOtherWithStudentsCourses(string instructorId, int courseId, int studentsNumber, int pageNumber, int pageSize)
        {
            var instructorCourses = _context.Courses.AsSplitQuery()
                    .Include(c => c.Instructor)
                    .Include(c => c.Enrollments)
                        .ThenInclude(e => e.Student)
                    .Where(c => c.InstructorId == instructorId && c.Id != courseId && !c.IsDeleted && c.Approved)
                    .Select(c => new StudentCourseDto
                    {
                        CourseId = c.Id,
                        Price = c.Price,
                        CourseTitle = c.Title,
                        CourseDescription = c.ShortDescription,
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
                            .Where(e => e.Student.ProfilePicture != null && !e.IsDeleted)
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
                    .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return instructorCourses.AsQueryable();
        }

        public async Task<Course> GetFeatureThisWeekCourse(int categoryId)
        {
            DateTime CutoffDate = DateTime.Now.AddDays(-7);
            var category = await _context.Categories.FindAsync(categoryId);
            if (category.Type == CategoryType.Topic)
            {
                var query = await _context.Courses.AsSplitQuery().Include(c => c.Instructor)
                .Include(c => c.Category)
                .Include(c => c.Enrollments.Where(en => en.EnrollmentDate >= CutoffDate))
                .Where(c => c.CategoryId == categoryId && !c.IsDeleted)
                .OrderByDescending(c => c.Enrollments.Count())
                .FirstOrDefaultAsync();
                if (query != null)
                    return query;
            }
            else
            {
                var topics = await _context.Categories.Where(cat => cat.ParentId == category.Id).ToListAsync();
                foreach (var topic in topics)
                {
                    if (topic.Type == CategoryType.Topic)
                    {
                        var queryFromTopic = await _context.Courses.AsSplitQuery().Include(c => c.Instructor)
                                .Include(c => c.Category)
                                .Include(c => c.Enrollments.Where(en => en.EnrollmentDate >= CutoffDate))
                                .Where(c => c.CategoryId == topic.Id && !c.IsDeleted)
                                .OrderByDescending(c => c.Enrollments.Count())
                                .FirstOrDefaultAsync();
                        if (queryFromTopic != null)
                            return queryFromTopic;
                    }
                    else
                    {
                        var certainTopics = await _context.Categories.Where(cat => cat.ParentId == topic.Id).ToListAsync();
                        foreach (var certainTopic in certainTopics)
                        {
                            var queryFromCertainTopic = await _context.Courses.AsSplitQuery().Include(c => c.Instructor)
                                    .Include(c => c.Category)
                                    .Include(c => c.Enrollments.Where(en => en.EnrollmentDate >= CutoffDate))
                                    .Where(c => c.CategoryId == certainTopic.Id && !c.IsDeleted)
                                    .OrderByDescending(c => c.Enrollments.Count())
                                    .FirstOrDefaultAsync();
                            if (queryFromCertainTopic != null)
                                return queryFromCertainTopic;
                        }
                    }
                }


            }
            throw new KeyNotFoundException("Feature this week not found");
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
            var courseInDb = await _context.Courses.AsSplitQuery()
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


        //to be replaced by filter later
        public IQueryable<Course> GetApprovedCoursesByInstructorAsync(string instructorId, int pageNumber, int pageSize)
        {
            var courses = _context.Courses.AsSplitQuery()
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                         .Where(c => c.InstructorId == instructorId && !c.IsDeleted && c.Approved)
                         .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return courses.AsQueryable();
        }

        public IQueryable<Course> GetNonApprovedCoursesByInstructorAsync(string instructorId, int pageNumber, int pageSize)
        {
            var courses = _context.Courses.AsSplitQuery()
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                         .Where(c => c.InstructorId == instructorId && !c.IsDeleted && !c.Approved)
                         .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return courses.AsQueryable();
        }

        public async Task<Course> PutCourseToApprovedAsync(int courseId)
        {
            var courseTobeApproved = await _context.Courses.FindAsync(courseId) ?? throw new Exception(string.Format(ErrorMessages.ResourceNotFound, $"Course with id {courseId}"));
            if (courseTobeApproved != null)
            {
                courseTobeApproved.Approved = true;
                _context.Courses.Update(courseTobeApproved);

                await _context.SaveChangesAsync();
            }
            return courseTobeApproved;
        }

        public IQueryable<Course> GetNonApprovedCoursesAsync(int pageNumber, int pageSize)
        {
            var courses = _context.Courses.AsSplitQuery()
                            .Include(c => c.CourseRequirements)
                            .Include(c => c.LearningItems)
                            .Include(c => c.EnrollmentItems)
                            .Include(c => c.Instructor)
                            .Include(c => c.Chapters)
                            .ThenInclude(c => c.Lessons)
                            .ThenInclude(l => l.Attachment)
                            .Include(c => c.Category)
                            .ThenInclude(c => c.Parent)
                            .ThenInclude(c => c.Parent)
                            .Where(c => !c.Approved)
                            .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return courses.AsQueryable();
        }


        private async Task<IQueryable<Course>> GetRelatedCoursesQueryAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId) ?? throw new Exception($"Course with id {courseId} not found.");
            int topicId = course.CategoryId;
            var subcategory = await _context.Categories
                .Include(c => c.Parent)
                    .ThenInclude(c => c.Parent)
                 .Where(c => c.Id == topicId && !c.IsDeleted)
                .FirstOrDefaultAsync() ?? throw new Exception($"SubCategory with id {topicId} not found.");
            int subcategoryId = (int)subcategory.ParentId;
            int categoryId = (int)subcategory.Parent.ParentId;

            var relatedCoursesQuery = _context.Courses
                .Where(c => (c.CategoryId == topicId ||
                            c.Category.ParentId == subcategoryId ||
                            c.Category.Parent.ParentId == categoryId) &&
                            c.Id != courseId && !c.IsDeleted && c.Approved);

            return relatedCoursesQuery;
        }

    }
}