using HealthApp.Model;

namespace HealthApp.Services
{
    public sealed class HomeDashboard
    {
        public Employee Employee { get; }
        public IReadOnlyList<Appointment> TodaysAppointments { get; }
        public Appointment? NextAppointment { get; }

        public HomeDashboard(Employee employee, List<Appointment> today, Appointment? next)
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            TodaysAppointments = today ?? throw new ArgumentNullException(nameof(today));
            NextAppointment = next;
        }
    }
}