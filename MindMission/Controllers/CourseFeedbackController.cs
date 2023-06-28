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
using MindMission.Infrastructure.Repositories;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseFeedbackController : ControllerBase
    {
        private readonly ICourseFeedbackService _courseFeedbackService;
        private readonly CourseFeedbackMappingService _courseFeedbackMappingService;
        public static int EntitiesCount { get; set; }
        static CourseFeedbackController()
        {
            EntitiesCount = 0;
        }

        public CourseFeedbackController(ICourseFeedbackService courseFeedbackService, CourseFeedbackMappingService courseFeedbackMappingService)
        {
            _courseFeedbackService = courseFeedbackService;
            _courseFeedbackMappingService = courseFeedbackMappingService;
        }
        #region Get
        [HttpGet("Course/{id:int}")]
        public async Task<IActionResult> GetFeedbackByCourseId([FromRoute] int id, [FromQuery] PaginationDto pagination)
        {
            var result = await _courseFeedbackService.GetFeedbackByCourseId(id);
            var courseFeedbacks = result.ToList();
            EntitiesCount = courseFeedbacks.Count;

            if (courseFeedbacks.Count > 0)
            {
                var courseFeedbakItems = courseFeedbacks.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                string successMessage = string.Format(SuccessMessages.RetrievedSuccessfully, "Course Feedback");
                return Ok(ResponseObjectFactory.CreateResponseObject(true, successMessage, _courseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbakItems).Result.ToList(), pagination.PageNumber, pagination.PageSize, EntitiesCount));
            }
            string message = string.Format(ErrorMessages.ResourceNotFound, "Course Feedback");
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, message, new List<CourseFeedbackWithCourseDto>()));

        }

        [HttpGet("Instructor/{id:Guid}")]
        public async Task<IActionResult> GetFeedbackByInstructorId([FromRoute] string id, [FromQuery] PaginationDto pagination)
        {
            var result = await _courseFeedbackService.GetFeedbackByInstructorId(id);
            var courseFeedbacks = result.ToList();
            EntitiesCount = courseFeedbacks.Count;

            if (courseFeedbacks.Count > 0)
            {
                var courseFeedbakItems = courseFeedbacks.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                string successMessage = string.Format(SuccessMessages.RetrievedSuccessfully, "Course Feedback");
                return Ok(ResponseObjectFactory.CreateResponseObject(true, successMessage, _courseFeedbackMappingService.SendMapEntityToDtoWithInstructor(courseFeedbakItems).Result.ToList(), pagination.PageNumber, pagination.PageSize, EntitiesCount));
            }
            string message = string.Format(ErrorMessages.ResourceNotFound, "Course Feedback");
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, message, new List<CourseFeedbackWithInstructorDto>()));
        }

        [HttpGet("InstructorCourse")]
        public async Task<IActionResult> GetFeedbackByCourseIdAndInstructorId([FromQuery] string InstructorId, [FromQuery] int CourseId, [FromQuery] PaginationDto pagination)
        {
            var result = await _courseFeedbackService.GetFeedbackByCourseIdAndInstructorId(CourseId, InstructorId);
            var courseFeedbacks = result.ToList();
            EntitiesCount = courseFeedbacks.Count;
            if (courseFeedbacks.Count > 0)
            {
                var courseFeedbakItems = courseFeedbacks.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                string successMessage = string.Format(SuccessMessages.RetrievedSuccessfully, "Course Feedback");
                return Ok(ResponseObjectFactory.CreateResponseObject(true, successMessage, _courseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbakItems).Result.ToList(), pagination.PageNumber, pagination.PageSize, EntitiesCount));
            }
            string message = string.Format(ErrorMessages.ResourceNotFound, "Course Feedback");
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, message, new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("Course")]
        public async Task<IActionResult> GetTopCoursesRating([FromQuery] int NumberOfCourses, [FromQuery] PaginationDto pagination)
        {
            var result = await _courseFeedbackService.GetTopCoursesRating(NumberOfCourses);
            var courseFeedbacks = result.ToList();
            EntitiesCount = courseFeedbacks.Count;

            if (courseFeedbacks.Count > 0)
            {
                var courseFeedbakItems = courseFeedbacks.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The top {NumberOfCourses} courses", _courseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbakItems).Result.ToList(), pagination.PageNumber, pagination.PageSize, EntitiesCount));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No Courses", new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("Instructor")]
        public async Task<IActionResult> GetTopInstructorsRating([FromQuery] int NumberOfInstructors, [FromQuery] PaginationDto pagination)
        {
            var result = await _courseFeedbackService.GetTopInstructorsRating(NumberOfInstructors);
            var courseFeedbacks = result.ToList();
            EntitiesCount = courseFeedbacks.Count;

            if (courseFeedbacks.Count > 0)
            {
                var courseFeedbakItems = courseFeedbacks.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The top {NumberOfInstructors} instructors", _courseFeedbackMappingService.SendMapEntityToDtoWithInstructor(courseFeedbakItems).Result.ToList(), pagination.PageNumber, pagination.PageSize, EntitiesCount));
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
    }
}
