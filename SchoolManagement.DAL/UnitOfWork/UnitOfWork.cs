using Microsoft.EntityFrameworkCore.Storage;
using SchoolManagement.DAL.Repositories;
using SchoolManagement.DAL.Repositories.Interfaces;
using SchoolManagement.DAL.UnitOfWork.Interfaces;

namespace SchoolManagement.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _schoolDbContext;

        private IDbContextTransaction _dbContextTransaction;

        private IStudentRepository _student;
        private ITeacherRepository _teacher;

        public UnitOfWork(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        public IStudentRepository Students
        {
            get
            {
                if (_student == null)
                {
                    _student = new StudentRepository(_schoolDbContext);
                }

                return _student;
            }
        }

        public ITeacherRepository Teachers
        {
            get
            {
                if ( _teacher == null)
                {
                    _teacher = new TeacherRepository(_schoolDbContext);
                }

                return _teacher;
            }
        }

        public void BeginTransaction()
        {
            _dbContextTransaction = _schoolDbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
            _dbContextTransaction.Dispose();
        }

        public int SaveChanges()
        {
            return _schoolDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _schoolDbContext.SaveChangesAsync();
        }

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _schoolDbContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
