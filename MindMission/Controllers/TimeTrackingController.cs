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
        public ActionResult<IQueryable<TimeTracking>> GetAllByStudentId(string studentId, [FromQuery] PaginationDto pagination)
        {
            var courseVisits = _service.GetByStudentId(studentId, pagination.PageNumber,pagination.PageSize);
            return Ok(courseVisits);
        }
        [HttpGet("course/{courseId}")]
        public  ActionResult<IQueryable<TimeTracking>> GetAllByCourseId(int courseId, [FromQuery] PaginationDto pagination)
        {
            var courseVisits = _service.GetByCourseId(courseId, pagination.PageNumber, pagination.PageSize);
            return Ok(courseVisits);
        }
        [HttpGet ("recentStudent/{courseId}")]
        public async Task<ActionResult<List<TimeTracking>>> GetRecentStudent(int courseId)
        {
            var students =  _service.GetLastfourStudentIds(courseId);
            return Ok(students);
        }
        [HttpGet("CourseCount/{courseId}")]
        public ActionResult<Object> GetCourseVisitCount(int courseId, [FromQuery] PaginationDto pagination)
        {
            var hourObject =  _service.GetCourseVisitCount(courseId, pagination.PageNumber, pagination.PageSize);
                return Ok(hourObject);
        }
        
        [HttpGet("hours/{instructorId}")]
        public async Task<ActionResult<long>> GetTotalHours(string instructorId, [FromQuery] PaginationDto pagination)
        {
            long hours = await _service.GetTotalHours(instructorId, pagination.PageNumber, pagination.PageSize);
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
