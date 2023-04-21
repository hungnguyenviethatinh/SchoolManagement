using SchoolManagement.DAL.Models;
using SchoolManagement.DAL.ViewModels;
using System.Linq.Expressions;

namespace SchoolManagement.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<int> AddTeachersAsync(IEnumerable<TeacherAddModel> model);

        IEnumerable<TeacherViewModel> GetTeachers(Expression<Func<Teacher, bool>>? filter = null);
    }
}
