using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolManagement.DAL.Models;
using SchoolManagement.DAL.UnitOfWork.Interfaces;
using SchoolManagement.DAL.ViewModels;
using SchoolManagement.Services.Interfaces;
using SchoolManagement.Shared.Settings;
using System.Linq.Expressions;

namespace SchoolManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentService> _logger;
        private readonly IMapper _mapper;
        private readonly AppSettings _settings;

        public StudentService(
            IUnitOfWork unitOfWork,
            ILogger<StudentService> logger,
            IMapper mapper,
            IOptions<AppSettings> options)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _settings = options.Value;
        }

        public async Task<int> AddStudentsAsync(IEnumerable<StudentAddModel> model)
        {
            try
            {
                var students = _mapper.Map<IEnumerable<Student>>(model);

                _unitOfWork.BeginTransaction();
                await _unitOfWork.Students.AddRangeAsync(students);

                var result = await _unitOfWork.SaveChangesAsync();

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on adding list of students.");
                _unitOfWork.Rollback();
            }

            return 0;
        }

        public IEnumerable<StudentViewModel> GetStudents(Expression<Func<Student, bool>>? filter = null)
        {
            var students = _unitOfWork.Students.Get(filter);

            return _mapper.Map<IEnumerable<StudentViewModel>>(students);
        }
    }
}
