using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Repositories;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseFeedbackController : ControllerBase
    {
        private readonly ICourseFeedbackService CourseFeedbackService;
        private readonly CourseFeedbackMappingService CourseFeedbackMappingService;

        public CourseFeedbackController(ICourseFeedbackService _CourseFeedbackService, CourseFeedbackMappingService _CourseFeedbackMappingService)
        {
            CourseFeedbackService = _CourseFeedbackService;
            CourseFeedbackMappingService = _CourseFeedbackMappingService;
        }

        [HttpGet("Course/{id:int}")]
        public async Task<IActionResult> GetFeedbackByCourseId([FromRoute] int id, [FromQuery] PaginationDto pagination)
        {
            var Result = await CourseFeedbackService.GetFeedbackByCourseId(id);
            var CourseFeedbacks = Result.ToList();
            if (CourseFeedbacks.Count > 0)
            {
                var courseFeedbakItems = CourseFeedbacks.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "All feedbacks for this course", CourseFeedbackMappingService.SendMapEntityToDtoWithCourse(courseFeedbakItems).Result.ToList(), 1, CourseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No feedbacks for this course", new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("Instructor/{id:Guid}")]
        public async Task<IActionResult> GetFeedbackByInstructorId([FromRoute] string id)
        {
            var Result = await CourseFeedbackService.GetFeedbackByInstructorId(id);
            var CourseFeedbacks = Result.ToList();
            if (CourseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "All feedbacks for this instructor", CourseFeedbackMappingService.SendMapEntityToDtoWithInstructor(CourseFeedbacks).Result.ToList(), 1, CourseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No feedbacks for this instructor", new List<CourseFeedbackWithInstructorDto>()));
        }

        [HttpGet("InstructorCourse")]
        public async Task<IActionResult> GetFeedbackByCourseIdAndInstructorId([FromQuery] string InstructorId, [FromQuery] int CourseId)
        {
            var Result = await CourseFeedbackService.GetFeedbackByCourseIdAndInstructorId(CourseId, InstructorId);
            var CourseFeedbacks = Result.ToList();
            if (CourseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "All feedbacks for this instructor in this course", CourseFeedbackMappingService.SendMapEntityToDtoWithCourse(CourseFeedbacks).Result.ToList(), 1, CourseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No feedbacks for this instructor in this course", new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("Course")]
        public async Task<IActionResult> GetTopCoursesRating([FromQuery] int NumberOfCourses)
        {
            var Result = await CourseFeedbackService.GetTopCoursesRating(NumberOfCourses);
            var CourseFeedbacks = Result.ToList();
            if (CourseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The top {NumberOfCourses} courses", CourseFeedbackMappingService.SendMapEntityToDtoWithCourse(CourseFeedbacks).Result.ToList(), 1, CourseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No Courses", new List<CourseFeedbackWithCourseDto>()));
        }

        [HttpGet("Instructor")]
        public async Task<IActionResult> GetTopInstructorsRating([FromQuery] int NumberOfInstructors)
        {
            var Result = await CourseFeedbackService.GetTopInstructorsRating(NumberOfInstructors);
            var CourseFeedbacks = Result.ToList();
            if (CourseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The top {NumberOfInstructors} instructors", CourseFeedbackMappingService.SendMapEntityToDtoWithInstructor(CourseFeedbacks).Result.ToList(), 1, CourseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No instructors", new List<CourseFeedbackWithInstructorDto>()));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCourseFeedback(AddCourseFeedbackDto CourseFeedbackDto)
        {
            var CourseFeedbackList = new List<AddCourseFeedbackDto>();
            if (ModelState.IsValid)
            {
                var Result = await CourseFeedbackService.AddCourseFeedback(CourseFeedbackMappingService.MapDtoToEntity(CourseFeedbackDto));
                if (Result != null)
                {
                    CourseFeedbackList.Add(CourseFeedbackMappingService.MapEntityToDto(Result).Result);
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "The feedback is added successfully", CourseFeedbackList));
                }

                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Result.ToString(), CourseFeedbackList));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), CourseFeedbackList));
        }
    }
}
