using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService _courseService)
        {
            courseService = _courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var permission = await courseService.GetAllAsync();

            ResponseObject<Course> AllCourse = new();
            AllCourse.ReturnedResponse(true, "All Courses", permission, 3, 10, permission.Count());

            return Ok(AllCourse);
        }
    }
}
