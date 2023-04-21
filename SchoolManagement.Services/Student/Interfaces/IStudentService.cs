using SchoolManagement.DAL.Models;
using SchoolManagement.DAL.ViewModels;
using System.Linq.Expressions;

namespace SchoolManagement.Services.Interfaces
{
    public interface IStudentService
    {
        Task<int> AddStudentsAsync(IEnumerable<StudentAddModel> model);

        IEnumerable<StudentViewModel> GetStudents(Expression<Func<Student, bool>>? filter = null);
    }
}
