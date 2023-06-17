using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
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
            if (courseFeedbacks.Count > 0)
            {
                var courseFeedbakItems = courseFeedbacks.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "All feedbacks for this course", _courseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbakItems).Result.ToList(), 1, courseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No feedbacks for this course", new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("Instructor/{id:Guid}")]
        public async Task<IActionResult> GetFeedbackByInstructorId([FromRoute] string id)
        {
            var result = await _courseFeedbackService.GetFeedbackByInstructorId(id);
            var courseFeedbacks = result.ToList();
            if (courseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "All feedbacks for this instructor", _courseFeedbackMappingService.SendMapEntityToDtoWithInstructor(courseFeedbacks).Result.ToList(), 1, courseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No feedbacks for this instructor", new List<CourseFeedbackWithInstructorDto>()));
        }

        [HttpGet("InstructorCourse")]
        public async Task<IActionResult> GetFeedbackByCourseIdAndInstructorId([FromQuery] string InstructorId, [FromQuery] int CourseId)
        {
            var result = await _courseFeedbackService.GetFeedbackByCourseIdAndInstructorId(CourseId, InstructorId);
            var courseFeedbacks = result.ToList();
            if (courseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "All feedbacks for this instructor in this course", _courseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbacks).Result.ToList(), 1, courseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No feedbacks for this instructor in this course", new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("Course")]
        public async Task<IActionResult> GetTopCoursesRating([FromQuery] int NumberOfCourses)
        {
            var result = await _courseFeedbackService.GetTopCoursesRating(NumberOfCourses);
            var courseFeedbacks = result.ToList();
            if (courseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The top {NumberOfCourses} courses", _courseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbacks).Result.ToList(), 1, courseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No Courses", new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("Instructor")]
        public async Task<IActionResult> GetTopInstructorsRating([FromQuery] int NumberOfInstructors)
        {
            var result = await _courseFeedbackService.GetTopInstructorsRating(NumberOfInstructors);
            var courseFeedbacks = result.ToList();
            if (courseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The top {NumberOfInstructors} instructors", _courseFeedbackMappingService.SendMapEntityToDtoWithInstructor(courseFeedbacks).Result.ToList(), 1, courseFeedbacks.Count));
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
