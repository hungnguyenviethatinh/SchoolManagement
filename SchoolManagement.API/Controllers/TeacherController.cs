using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DAL.ViewModels;
using SchoolManagement.Services.Interfaces;

namespace SchoolManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private ILogger<TeacherController> _logger;
        private ITeacherService _teacherService;

        public TeacherController(ILogger<TeacherController> logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        [HttpPost("AddTeachers")]
        public async Task<IActionResult> AddTeachers([FromBody] IEnumerable<TeacherAddModel> model)
        {
            if (ModelState.IsValid)
            {
                var result = await _teacherService.AddTeachersAsync(model);

                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("GetTeachers")]
        public IActionResult GetTeachers([FromQuery] string? teacherName)
        {
            var teachers = _teacherService.GetTeachers(
                filter: teacher => string.IsNullOrEmpty(teacherName) || teacher.Name.Contains(teacherName));

            return Ok(teachers);
        }
    }
}
