using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.DAL
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly SchoolDbContext _context;

        public DatabaseInitializer(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
