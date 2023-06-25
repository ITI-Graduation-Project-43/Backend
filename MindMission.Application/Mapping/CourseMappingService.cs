using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class CourseMappingService : IMappingService<Course, CourseDto>
    {

        public CourseMappingService()
        {
        }

        public async Task<CourseDto> MapEntityToDto(Course course)
        {
            var courseDTO = new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                Description = course.Description,
                ImageUrl = course.ImageUrl,
                Language = (Language)Enum.Parse(typeof(Language), course.Language),
                Price = course.Price,
                Level = (Level)Enum.Parse(typeof(Level), course.Level),
                AvgReview = course.AvgReview,
                NoOfReviews = course.NoOfReviews,
                NoOfStudents = course.NoOfStudents,
                Discount = course.Discount,
                ChapterCount = course.ChapterCount,
                LessonCount = course.LessonCount,
                NoOfVideos = course.NoOfVideos,
                NoOfArticles = course.NoOfArticles,
                NoOfAttachments = course.NoOfAttachments,
                NoOfQuizes = course.NoOfQuizzes,
                NoOfHours = course.NoOfHours,
                Published = course.Published,
                Approved = course.Approved,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt,
            };

            if (course.Instructor != null)
            {
                courseDTO.InstructorId = course.Instructor.Id;
                courseDTO.InstructorName = course.Instructor.FirstName + " " + course.Instructor.LastName;
                courseDTO.InstructorBio = course.Instructor.Bio;
                courseDTO.InstructorProfilePicture = course.Instructor.ProfilePicture;
                courseDTO.InstructorTitle = course.Instructor.Title;
                courseDTO.InstructorDescription = course.Instructor.Description;
                courseDTO.InstructorNoOfCourses = course.Instructor.Courses.Count;
                courseDTO.InstructorNoOfStudents = course.Instructor.NoOfStudents;
                courseDTO.InstructorAvgRating = course.Instructor.AvgRating;
                courseDTO.InstructorNoOfRatings = course.Instructor.NoOfRatings;
            }


            if (course.Category != null)
            {
                courseDTO.TopicName = course.Category.Name;
                courseDTO.TopicId = course.Category.Id;
                if (course.Category.Parent != null)
                {
                    courseDTO.SubCategoryName = course.Category.Parent.Name;
                    courseDTO.SubCategoryId = course.Category.Parent.Id;

                    if (course.Category.Parent.Parent != null)
                    {
                        courseDTO.CategoryName = course.Category.Parent.Parent.Name;
                        courseDTO.CategoryId = course.Category.Parent.Parent.Id;
                    }
                }
            }

            // Map the chapters
            courseDTO.Chapters = course.Chapters?.Select(chapter => new CourseChapterDto
            {
                Title = chapter.Title,
                NoOfLessons = chapter.NoOfLessons,
                NoOfHours = chapter.NoOfHours,
            }).ToList();

            courseDTO.LearningItems = course.LearningItems?.Select(i => new LearningItemDto { Title = i?.Title, Description = i?.Description }).ToList();
            courseDTO.EnrollmentItems = course.EnrollmentItems?.Select(i => new EnrollmentItemDto { Description = i?.Description }).ToList();
            courseDTO.CourseRequirements = course.CourseRequirements?.Select(r => new CourseRequirementDto { Title = r?.Title, Description = r?.Description }).ToList();

            return courseDTO;
        }

        public Course MapDtoToEntity(CourseDto courseDTO)
        {
            return new Course
            {
                Id = courseDTO.Id,
                Title = courseDTO.Title,
                ShortDescription = courseDTO.ShortDescription,
                Description = courseDTO.Description,
                ImageUrl = courseDTO.ImageUrl,
                Language = courseDTO.Language.ToString(),
                Price = courseDTO.Price,
                Level = courseDTO.Level.ToString(),
                Discount = courseDTO.Discount,
                Published = courseDTO.Published,
                Approved = courseDTO.Approved,
                UpdatedAt = DateTime.Now,
                CategoryId = courseDTO.TopicId,
                InstructorId = courseDTO.InstructorId,
                LearningItems = courseDTO.LearningItems?.Select(i => new LearningItem { Title = i.Title, Description = i.Description }).ToList(),
                EnrollmentItems = courseDTO.EnrollmentItems?.Select(i => new EnrollmentItem { Description = i.Description }).ToList(),
                CourseRequirements = courseDTO.CourseRequirements?.Select(r => new CourseRequirement { Title = r.Title, Description = r.Description }).ToList(),
            };
        }
    }
}