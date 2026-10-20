
using HealthApp.Model;
/// <summary>
/// Represents a repository for managing appointment data.
/// </summary>
public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAllAsync(int empId,
        CancellationToken cancellationToken = default);

    Task<Appointment?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<Patient?> GetPatientByIdAsync(
        int patientId,
        CancellationToken cancellationToken = default);

    Task<List<Appointment>> GetByPatientIdAsync(
        int patientId,
        CancellationToken cancellationToken = default);

    Task<List<Appointment>> GetTodaysAppointmentAsync(int empId,
        CancellationToken cancellationToken = default);

    Task<Appointment?> GetNextAppointmentAsync(int empId,
        CancellationToken cancellationToken = default);
        

    Task<Appointment?> AddAppointment(Appointment appointment);
    Task<Appointment?> UpdateAppointment(Appointment appointment);
    Task<Appointment?> DeleteAppointment(int id);
}