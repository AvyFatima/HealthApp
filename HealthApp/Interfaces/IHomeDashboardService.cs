using HealthApp.Services;

namespace HealthApp.Interfaces;

public interface IHomeDashboardService
{
    Task<HomeDashboard> GetDashboardAsync(
        int employeeId,
        CancellationToken cancellationToken = default);

}
