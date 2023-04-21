using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolManagement.DAL.UnitOfWork.Interfaces;
using SchoolManagement.DAL.ViewModels;
using SchoolManagement.Services.Interfaces;

namespace SchoolManagement.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DashboardService> _logger;
        private readonly IMapper _mapper;

        public DashboardService(
            IUnitOfWork unitOfWork,
            ILogger<DashboardService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<DashboardViewModel> GetDashboard()
        {
            var teachersWithStudents = _unitOfWork.Teachers.Get(
                orderBy: query => query.OrderBy(teacher => teacher.Name),
                includeProperties: "Students");

            return _mapper.Map<IEnumerable<DashboardViewModel>>(teachersWithStudents);
        }
    }
}
