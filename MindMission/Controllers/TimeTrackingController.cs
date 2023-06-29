using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTrackingController : ControllerBase
    {
        private readonly ITimeTrackingService _service;
        public TimeTrackingController(ITimeTrackingService service)
        {
            _service= service;
        }

        #region Get
        [HttpGet ("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<TimeTracking>>> GetAllByStudentId(string studentId)
        {
            var courseVisits = await _service.GetByStudentId(studentId);
            return Ok(courseVisits);
        }
        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<TimeTracking>>> GetAllByCourseId(int courseId)
        {
            var courseVisits = await _service.GetByCourseId(courseId);
            return Ok(courseVisits);
        }
        [HttpGet ("recentStudent/{courseId}")]
        public async Task<ActionResult<List<TimeTracking>>> GetRecentStudent(int courseId)
        {
            var students = await _service.GetLastfourStudentIds(courseId);
            return Ok(students);
        }
        [HttpGet("CourseCount/{courseId}")]
        public async Task<ActionResult<Object>> GetCourseVisitCount(int courseId)
        {
            var hourObject = await _service.GetCourseVisitCount(courseId);
            return Ok(hourObject);
        }
        
        [HttpGet("hours/{instructorId}")]
        public async Task<ActionResult<long>> GetTotalHours(string instructorId)
        {
            long hours = await _service.GetTotalHours(instructorId);
            return Ok(hours);
        }
        
        #endregion

        #region Create
        [HttpPost("course/{courseId}/student/{studentId}")]
        public async Task<ActionResult> StartTime(string studentId , int courseId)
        {
            var createdCourseVisit = await _service.Create(studentId, courseId);
            return Ok(createdCourseVisit);
        }
        #endregion

        #region update
        [HttpPut("course/{courseId}/student/{studentId}")]
        public async Task<ActionResult> EndTime(string studentId, int courseId)
        {
            var createdCourseVisit = await _service.Update(studentId, courseId);
            return Ok(createdCourseVisit);
        }
        #endregion

    }
}
