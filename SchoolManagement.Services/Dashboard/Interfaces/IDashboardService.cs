using SchoolManagement.DAL.ViewModels;

namespace SchoolManagement.Services.Interfaces
{
    public interface IDashboardService
    {
        IEnumerable<DashboardViewModel> GetDashboard();
    }
}
