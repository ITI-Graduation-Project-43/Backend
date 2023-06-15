using Azure.Storage.Blobs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Mapping;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController<Course, CourseDto, int>
    {
        private readonly ICourseService _courseService;
        private readonly IMappingService<Course, CourseCreateDto> _postCourseMappingService;
        private readonly BlobContainerClient containerClient;
        public CourseController(ICourseService courseService, CourseMappingService courseMappingService, IMappingService<Course, CourseCreateDto> postCourseMappingService, BlobServiceClient blobServiceClient, IConfiguration configuration) : base(courseMappingService)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _postCourseMappingService = postCourseMappingService ?? throw new ArgumentNullException(nameof(postCourseMappingService));
            string containerName = configuration["AzureStorage:ContainerName2"];
            containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        #region Get

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithInclude(
               _courseService.GetAllAsync,
               pagination,
               "Courses",
               course => course.Instructor,
               Course => Course.Category,
               Course => Course.Chapters,
               Course => Course.Category.Parent,
               Course => Course.Category.Parent.Parent,
               Course => Course.CourseRequirements,
               Course => Course.LearningItems,
               Course => Course.EnrollmentItems
           );
        }

        // GET: api/Course/category/{categoryId}

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByCategory(int categoryId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetAllByCategoryAsync(categoryId), pagination, "Courses");
        }

        // GET: api/Course/{courseId}/related

        [HttpGet("{courseId}/related")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetRelatedCourses(int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetRelatedCoursesAsync(courseId), pagination, "Courses");
        }

        // GET: api/Course/instructor/{instructorId}

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByInstructor(string instructorId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetAllByInstructorAsync(instructorId), pagination, "Courses");
        }

        // GET: api/Course/instructorOtherCourses/{instructorId}

        [HttpGet("{courseId}/instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetInstructorOtherCourses(string instructorId, int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetInstructorOtherCourses(instructorId, courseId), pagination, "Courses");
        }

        // GET: api/Course/top/{topNumber}

        [HttpGet("top/{topNumber}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetTopRatedCourses(int topNumber, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetTopRatedCoursesAsync(topNumber), pagination, "Courses");
        }

        // GET: api/Course/recent/{recentNumber}

        [HttpGet("recent/{recentNumber}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetRecentCourses(int recentNumber, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetRecentCoursesAsync(recentNumber), pagination, "Courses");
        }

        // GET: api/Course/{courseId}/related/{studentsNumber}

        [HttpGet("{courseId}/related/{studentsNumber}")]
        public async Task<ActionResult<IQueryable<CourseDto>>> GetRelatedCoursesWithStudentsAsync(int courseId, int studentsNumber, [FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetRelatedCoursesWithStudentsAsync(courseId, studentsNumber);

            if (courses == null)
            {
                return NotFound(NotFoundResponse("Courses"));
            }


            var coursesList = courses.ToList();
            if (coursesList.Count == 0)
            {
                return NotFound(NotFoundResponse("Courses"));
            }
            var coursesPage = coursesList.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();

            var response = ResponseObjectFactory.CreateResponseObject(true, string.Format(SuccessMessages.RetrievedSuccessfully, "Courses"), coursesPage, pagination.PageNumber, pagination.PageSize);
            return Ok(response);
        }


        [HttpGet("{courseId}/instructor/{instructorId}/{studentsNumber}")]
        public async Task<ActionResult<IQueryable<StudentCourseDto>>> GetInstructorOtherCoursesWithStudentsAsync(string instructorId, int courseId, int studentsNumber, [FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetInstructorOtherWithStudentsCourses(instructorId, courseId, studentsNumber);

            if (courses == null)
            {
                return NotFound(NotFoundResponse("Courses"));
            }

            var coursesList = courses.ToList();
            if (coursesList.Count == 0)
            {
                return NotFound(NotFoundResponse("Courses"));
            }
            var coursesPage = coursesList.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();

            var response = ResponseObjectFactory.CreateResponseObject(true, string.Format(SuccessMessages.RetrievedSuccessfully, "Courses"), coursesPage, pagination.PageNumber, pagination.PageSize);
            return Ok(response);
        }

        [HttpGet("{courseId}/students/{studentsNumber}")]
        public async Task<ActionResult<StudentCourseDto>> GetCourseByIdWithStudentsAsync(int courseId, int studentsNumber)
        {
            var course = await _courseService.GetCourseByIdWithStudentsAsync(courseId, studentsNumber);

            if (course == null)
            {
                return NotFound(NotFoundResponse("Course"));
            }

            var response = ResponseObjectFactory.CreateResponseObject(true, string.Format(SuccessMessages.RetrievedSuccessfully, "Course"), new List<StudentCourseDto>() { course });
            return Ok(response);
        }

        // GET: api/Course/{courseId}
        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(int courseId)
        {
            return await GetEntityResponseWithInclude(
                    () => _courseService.GetByIdAsync(courseId,
                        course => course.Instructor,
                        Course => Course.Category,
                        Course => Course.Chapters,
                        Course => Course.Category.Parent,
                        Course => Course.Category.Parent.Parent,
                        Course => Course.CourseRequirements,
                        Course => Course.LearningItems,
                        Course => Course.EnrollmentItems


                    ),
                    "Course"
                );
        }

        // GET: api/Course/name/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<CourseDto>> GetCourseByName(string name)
        {
            return await GetEntityResponse(() => _courseService.GetByNameAsync(name), "Course");
        }

        // GET: api/Course/featureThisWeek
        [HttpGet("featureThisWeek")]
        public async Task<ActionResult<CourseDto>> GetFeatureThisWeekCourse()
        {
            return await GetEntityResponse(() => _courseService.GetFeatureThisWeekCourse(), "Course");
        }


        #endregion Get

        #region Add

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<CourseDto>> AddCourse([FromBody] CourseDto courseDTO)
        {
            return await AddEntityResponse(_courseService.AddAsync, courseDTO, "Course", nameof(GetCourseById));
        }

        [HttpPost("Test")]
        public async Task<IActionResult> Post([FromBody] CourseCreateDto postCourseDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                //return BadRequest(InvalidDataResponse());

            }

            if (!Enum.IsDefined(typeof(CategoryType), postCourseDto.Category) || postCourseDto.Category != CategoryType.Topic)
            {
                return BadRequest("Invalid Category.");
            }


            if (!Enum.IsDefined(typeof(Language), postCourseDto.Language))
            {
                return BadRequest("Invalid Language.");
            }

            if (!Enum.IsDefined(typeof(Level), postCourseDto.Level))
            {
                return BadRequest("Invalid Level.");
            }

            if (postCourseDto.LearningItems == null || !postCourseDto.LearningItems.Any() || postCourseDto.LearningItems.Any(item => string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Description)))
            {
                return BadRequest("LearningItems are invalid.");
            }

            if (postCourseDto.EnrollmentItems == null || !postCourseDto.EnrollmentItems.Any() || postCourseDto.EnrollmentItems.Any(item => string.IsNullOrWhiteSpace(item.Title)))
            {
                return BadRequest("EnrollmentItems are invalid.");
            }
            // Check if a file is uploaded
            if (courseImg == null || courseImg.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            // Validate the file size
            if (courseImg.Length > 10_000_000) // 10 MB
            {
                return BadRequest("The file is too large. The maximum allowed size is 10 MB.");
            }

            // Validate the file extension
            var allowedExtensions = new[] { ".jpg", ".png", ".webp", ".jpeg", ".avif" };
            var extension = Path.GetExtension(courseImg.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                return BadRequest($"Invalid file extension. Allowed extensions are: {string.Join(", ", allowedExtensions)}");
            }

            // Upload the file and save the URL
            string fileName = Guid.NewGuid().ToString() + extension;

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            using (Stream stream = courseImg.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            postCourseDto.CourseImage = blobClient.Uri.ToString();

            // Map the course create DTO to a course entity.
            var courseToCreate = _postCourseMappingService.MapDtoToEntity(postCourseDto);

            // Use the service to add the course to the database.
            var createdCourse = await _courseService.AddCourseAsync(courseToCreate);

            // Map the created course entity back to a DTO.
            var courseDto = _postCourseMappingService.MapEntityToDto(createdCourse);

            if (courseDto == null)
                return NotFound(NotFoundResponse("course"));
            // Return the created course.
            return CreatedAtRoute(new { id = courseDto.Id }, courseDto);








            //string message = string.Format(SuccessMessages.CreatedSuccessfully, "Course");
            //var response = ResponseObjectFactory.CreateResponseObject<CourseCreateDto>(true, message, new List<CourseCreateDto> { courseDto });
            //return CreatedAtAction(nameof(GetCourseById), new { id = courseDto.Id }, response);
        }

        #endregion Add

        #region Delete

        // DELETE: api/Course/{courseId}
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            return await DeleteEntityResponse(_courseService.GetByIdAsync, _courseService.DeleteAsync, courseId);
        }

        #endregion Delete

        #region Edit Patch/Put

        // PUT: api/Course/{courseId}
        [HttpPut("{courseId}")]
        public async Task<ActionResult> UpdateCourse(int courseId, CourseDto courseDto)
        {
            return await UpdateEntityResponse(_courseService.GetByIdAsync, _courseService.UpdateAsync, courseId, courseDto, "Course");
        }

        // PATCH: api/Course/{courseId}
        [HttpPatch("{courseId}")]
        public async Task<ActionResult> PatchCourse(int courseId, [FromBody] JsonPatchDocument<CourseDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            return await PatchEntityResponse(_courseService.GetByIdAsync, _courseService.UpdateAsync, courseId, "Course", patchDocument);
        }

        #endregion Edit Patch/Put
    }
}