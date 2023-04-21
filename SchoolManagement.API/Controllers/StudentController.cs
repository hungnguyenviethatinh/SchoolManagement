using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DAL.ViewModels;
using SchoolManagement.Services.Interfaces;

namespace SchoolManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;

        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpPost("AddStudents")]
        public async Task<IActionResult> AddStudents([FromBody] IEnumerable<StudentAddModel> model)
        {
            if (ModelState.IsValid)
            {
                var result = await _studentService.AddStudentsAsync(model);

                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("GetStudents")]
        public IActionResult GetStudents([FromQuery] string? studentName)
        {
            var students = _studentService.GetStudents(
                filter: student => string.IsNullOrWhiteSpace(studentName) || student.Name.Contains(studentName));

            return Ok(students);
        }
    }
}