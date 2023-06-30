using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.CourseFeedbackDtos;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Domain.Constants;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseFeedbackController : ControllerBase
    {
        private readonly ICourseFeedbackService _courseFeedbackService;
        private readonly CourseFeedbackMappingService _courseFeedbackMappingService;


        public CourseFeedbackController(ICourseFeedbackService courseFeedbackService, CourseFeedbackMappingService courseFeedbackMappingService)
        {
            _courseFeedbackService = courseFeedbackService;
            _courseFeedbackMappingService = courseFeedbackMappingService;
        }
        #region Get
        [HttpGet("Course/{id:int}")]
        public async Task<IActionResult> GetFeedbackByCourseId([FromRoute] int id, [FromQuery] PaginationDto pagination)
        {
            var result = _courseFeedbackService.GetFeedbackByCourseId(id, pagination.PageNumber, pagination.PageSize);
            var courseFeedbacks = result.ToList();
            var totalCount = await _courseFeedbackService.GetTotalFeedbackCountByCourseId(id);


            if (courseFeedbacks.Count > 0)
            {
                string successMessage = string.Format(SuccessMessages.RetrievedSuccessfully, "Course Feedback");
                return Ok(ResponseObjectFactory.CreateResponseObject(true, successMessage, _courseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbacks).Result.ToList(), pagination.PageNumber, pagination.PageSize, totalCount));
            }
            string message = string.Format(ErrorMessages.ResourceNotFound, "Course Feedback");
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, message, new List<CourseFeedbackWithCourseDto>()));

        }

        [HttpGet("Instructor/{id:Guid}")]
        public async Task<IActionResult> GetFeedbackByInstructorId([FromRoute] string id, [FromQuery] PaginationDto pagination)
        {
            var result = _courseFeedbackService.GetFeedbackByInstructorId(id, pagination.PageNumber, pagination.PageSize);
            var courseFeedbacks = result.ToList();
            var totalCount = await _courseFeedbackService.GetTotalCountAsync();

            if (courseFeedbacks.Count > 0)
            {
                string successMessage = string.Format(SuccessMessages.RetrievedSuccessfully, "Course Feedback");
                return Ok(ResponseObjectFactory.CreateResponseObject(true, successMessage, _courseFeedbackMappingService.SendMapEntityToDtoWithInstructor(courseFeedbacks).Result.ToList(), pagination.PageNumber, pagination.PageSize, totalCount));
            }
            string message = string.Format(ErrorMessages.ResourceNotFound, "Course Feedback");
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, message, new List<CourseFeedbackWithInstructorDto>()));
        }

        [HttpGet("InstructorCourse")]
        public async Task<IActionResult> GetFeedbackByCourseIdAndInstructorId([FromQuery] string InstructorId, [FromQuery] int CourseId, [FromQuery] PaginationDto pagination)
        {
            var result = _courseFeedbackService.GetFeedbackByCourseIdAndInstructorId(CourseId, InstructorId, pagination.PageNumber, pagination.PageSize);
            var courseFeedbacks = result.ToList();
            var totalCount = await _courseFeedbackService.GetTotalFeedbackCountByCourseIdAndInstructorId(CourseId, InstructorId);
            if (courseFeedbacks.Count > 0)
            {
                string successMessage = string.Format(SuccessMessages.RetrievedSuccessfully, "Course Feedback");
                return Ok(ResponseObjectFactory.CreateResponseObject(true, successMessage, _courseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbacks).Result.ToList(), pagination.PageNumber, pagination.PageSize, totalCount));
            }
            string message = string.Format(ErrorMessages.ResourceNotFound, "Course Feedback");
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, message, new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("StudentFeedback")]
        public async Task<IActionResult> GetFeedbackByCourseIdAndStudentId([FromQuery] string studentId, [FromQuery] int courseId, [FromQuery] PaginationDto pagination)
        {
            var courseFeedback = await _courseFeedbackService.GetFeedbackByCourseIdAndStudentId(courseId, studentId);
            if (courseFeedback != null)
            {
                string successMessage = string.Format(SuccessMessages.RetrievedSuccessfully, "Course Feedback");
                return Ok(ResponseObjectFactory.CreateResponseObject(true, successMessage, new List<AddCourseFeedbackDto> { courseFeedback }));
            }
            string message = string.Format(ErrorMessages.ResourceNotFound, "Course Feedback");
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, message, new List<CourseFeedbackWithCourseDto>()));
        }


        [HttpGet("Course")]
        public async Task<IActionResult> GetTopCoursesRating([FromQuery] int NumberOfCourses, [FromQuery] PaginationDto pagination)
        {
            var result = _courseFeedbackService.GetTopCoursesRating(NumberOfCourses, pagination.PageNumber, pagination.PageSize);
            var courseFeedbacks = result.ToList();
            var totalCount = await _courseFeedbackService.GetTotalCountAsync();

            if (courseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The top {NumberOfCourses} courses", _courseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbacks).Result.ToList(), pagination.PageNumber, pagination.PageSize, totalCount));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No Courses", new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("Instructor")]
        public async Task<IActionResult> GetTopInstructorsRating([FromQuery] int NumberOfInstructors, [FromQuery] PaginationDto pagination)
        {
            var result = _courseFeedbackService.GetTopInstructorsRating(NumberOfInstructors, pagination.PageNumber, pagination.PageSize);
            var courseFeedbacks = result.ToList();
            var totalCount = await _courseFeedbackService.GetTotalCountAsync();

            if (courseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The top {NumberOfInstructors} instructors", _courseFeedbackMappingService.SendMapEntityToDtoWithInstructor(courseFeedbacks).Result.ToList(), pagination.PageNumber, pagination.PageSize, totalCount));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No instructors", new List<CourseFeedbackWithInstructorDto>()));
        }
        #endregion

        #region Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddCourseFeedback(AddCourseFeedbackDto CourseFeedbackDto)
        {
            var courseFeedbackList = new List<AddCourseFeedbackDto>();
            if (ModelState.IsValid)
            {
                var Result = await _courseFeedbackService.AddCourseFeedback(_courseFeedbackMappingService.MapDtoToEntity(CourseFeedbackDto));
                if (Result != null)
                {
                    courseFeedbackList.Add(_courseFeedbackMappingService.MapEntityToDto(Result).Result);
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "The feedback is added successfully", courseFeedbackList));
                }

                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Result.ToString(), courseFeedbackList));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), courseFeedbackList));
        }
        #endregion

        #region Delete

        #endregion


        // PUT: api/CourseFeedback/{Id}
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCourseFeedback(int Id, [FromBody] CourseFeedback feedback)
        {
            if (ModelState.IsValid)
            {
                var Result = await _courseFeedbackService.UpdateCourseFeedback(Id, feedback);
                if (Result != null)
                {
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "The feedback is updated successfully", new List<CourseFeedback> { Result }));
                }

                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Result.ToString(), new List<CourseFeedback> { }));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<CourseFeedback> { }));
        }
    }
}
