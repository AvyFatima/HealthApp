using System.Globalization;
using HealthApp.Interfaces;
using HealthApp.Model;

namespace HealthApp.Services;

public sealed class HomeDashboardService : IHomeDashboardService
{
    private readonly IEmployeeRepository _employees;
    private readonly IAppointmentService _appointmentservice;

    public HomeDashboardService(IEmployeeRepository employees, IAppointmentService appointmentservice)
    {
        _employees = employees;
        _appointmentservice = appointmentservice;
    }

    /// <summary>
    /// Gets the home dashboard for the specified employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee for whom to get the dashboard.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the home dashboard for the specified employee.</returns>
    public async Task<HomeDashboard> GetDashboardAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        var emp = await _employees.GetByIdAsync(employeeId, cancellationToken)
            ?? throw new InvalidOperationException("The signed-in employee could not be found.");

        var today = _appointmentservice.GetTodaysAppointment(employeeId, cancellationToken);
        var next = _appointmentservice.GetNextAppointment(employeeId, cancellationToken);

        return new HomeDashboard(emp, (List<Appointment>)today, next);
    }
}
