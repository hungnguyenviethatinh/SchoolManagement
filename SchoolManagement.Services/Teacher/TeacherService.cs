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
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TeacherService> _logger;
        private readonly IMapper _mapper;
        private readonly AppSettings _settings;

        public TeacherService(
            IUnitOfWork unitOfWork,
            ILogger<TeacherService> logger,
            IMapper mapper,
            IOptions<AppSettings> options)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _settings = options.Value;
        }

        public async Task<int> AddTeachersAsync(IEnumerable<TeacherAddModel> model)
        {
            try
            {
                var teachers = _mapper.Map<IEnumerable<Teacher>>(model);

                _unitOfWork.BeginTransaction();
                await _unitOfWork.Teachers.AddRangeAsync(teachers);

                var result = await _unitOfWork.SaveChangesAsync();

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on add list of teachers.");
                _unitOfWork.Rollback();
            }

            return 0;
        }

        public IEnumerable<TeacherViewModel> GetTeachers(Expression<Func<Teacher, bool>>? filter = null)
        {
            var teachers = _unitOfWork.Teachers.Get(filter);

            return _mapper.Map<IEnumerable<TeacherViewModel>>(teachers);
        }
    }
}
