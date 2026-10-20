using HealthApp.Model;

namespace HealthApp.Interfaces;

public interface IAppointmentService
{
    IEnumerable<Appointment> GetAllAppointments(int empId, CancellationToken cancellationToken = default);

    IEnumerable<Appointment> GetTodaysAppointment(int empId,
            CancellationToken cancellationToken = default);

    Appointment? GetNextAppointment(int empId,
            CancellationToken cancellationToken = default);

    Appointment? GetById(int id, CancellationToken cancellationToken = default);

    IEnumerable<Appointment> GetAppointmentsByPatientId(int patientId, CancellationToken cancellationToken = default);

    Patient? GetPatientById(int patientId, CancellationToken cancellationToken = default);

    
    Appointment? AddAppointment(Appointment appointment);

    Appointment? UpdateAppointment(Appointment appointment);

    Appointment? DeleteAppointment(int id);
}