using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interfaces;

namespace SchoolManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private ILogger<DashboardController> _logger;
        private IDashboardService _dashboardService;

        public DashboardController(ILogger<DashboardController> logger, IDashboardService dashboardService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        [HttpGet("GetDashboard")]
        public IActionResult GetDashboard()
        {
            var dashboard = _dashboardService.GetDashboard();

            return Ok(dashboard);
        }
    }
}
