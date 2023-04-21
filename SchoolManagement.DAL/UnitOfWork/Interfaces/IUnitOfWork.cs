using SchoolManagement.DAL.Repositories.Interfaces;

namespace SchoolManagement.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }

        ITeacherRepository Teachers { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
