using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
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
        private readonly IMappingService<CourseFeedback, CourseFeedbackDto> CourseFeedbackMappingService;

        public CourseFeedbackController(ICourseFeedbackService _CourseFeedbackService, IMappingService<CourseFeedback, CourseFeedbackDto> _CourseFeedbackMappingService)
        {
            CourseFeedbackService = _CourseFeedbackService;
            CourseFeedbackMappingService = _CourseFeedbackMappingService;
        }

        [HttpGet("Course/{id:int}")]
        public async Task<IActionResult> GetFeedbackByCourseId([FromRoute] int id)
        {
            var Result = await CourseFeedbackService.GetFeedbackByCourseId(id);
            var CourseFeedbacks = Result.ToList();
            if (CourseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "All feedbacks", CourseFeedbacks, 1, CourseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No feedbacks for this course", CourseFeedbacks));
        }

        [HttpGet("Instructor/{id:Guid}")]
        public async Task<IActionResult> GetFeedbackByInstructorId([FromRoute] string id)
        {
            var Result = await CourseFeedbackService.GetFeedbackByInstructorId(id);
            var CourseFeedbacks = Result.ToList();
            if (CourseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "All feedbacks", CourseFeedbacks, 1, CourseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No feedbacks for this instructors", CourseFeedbacks));
        }

        [HttpGet("Course")]
        public async Task<IActionResult> GetTopCoursesRating([FromQuery] int NumberOfCourses)
        {
            var Result = await CourseFeedbackService.GetTopCoursesRating(NumberOfCourses);
            var CourseFeedbacks = Result.ToList();
            if (CourseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The Top {NumberOfCourses} Courses", CourseFeedbacks, 1, CourseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No Courses", CourseFeedbacks));
        }

        [HttpGet("Instructor")]
        public async Task<IActionResult> GetTopInstructorsRating([FromQuery] int NumberOfInstructors)
        {
            var Result = await CourseFeedbackService.GetTopInstructorsRating(NumberOfInstructors);
            var CourseFeedbacks = Result.ToList();
            if (CourseFeedbacks.Count > 0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, $"The Top {NumberOfInstructors} Instructors", CourseFeedbacks, 1, CourseFeedbacks.Count));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "No instructors", CourseFeedbacks));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCourseFeedback(CourseFeedbackDto CourseFeedbackDto)
        {
            var CourseFeedbackList = new List<CourseFeedbackDto>();
            if(ModelState.IsValid)
            {
                var Result = await CourseFeedbackService.AddCourseFeedback(CourseFeedbackMappingService.MapDtoToEntity(CourseFeedbackDto));
                if (Result != null)
                {
                    CourseFeedbackList.Add(CourseFeedbackMappingService.MapEntityToDto(Result).Result);
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "The feedback is added successfully", CourseFeedbackList));
                }
                Console.WriteLine("*******************************************************************");
                Console.WriteLine(Result);
                Console.WriteLine("*******************************************************************");

                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Result.ToString(), CourseFeedbackList));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), CourseFeedbackList));
        }
    }
}
