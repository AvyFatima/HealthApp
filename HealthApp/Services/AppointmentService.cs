using System.Threading;
using HealthApp.Interfaces;
using HealthApp.Model;

namespace HealthApp;

/// <summary>
/// Represents a service for managing appointment data.
/// </summary>
public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository)
    {
        _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
    }

    /// <summary>
    /// Retrieves all appointments for a specific employee.
    /// </summary>
    /// <param name="empId">The ID of the employee.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An IEnumerable of appointments for the specified employee.</returns>
    public IEnumerable<Appointment> GetAllAppointments(int empId, CancellationToken cancellationToken = default)
    {
        return _appointmentRepository.GetAllAsync(empId, cancellationToken).Result;
    }

    /// <summary>
    /// Retrieves today's appointments for a specific employee.
    /// </summary>
    /// <param name="empId">The ID of the employee.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An IEnumerable of today's appointments for the specified employee.</returns
    public IEnumerable<Appointment> GetTodaysAppointment(int empId,
            CancellationToken cancellationToken = default)
    {
        return _appointmentRepository.GetTodaysAppointmentAsync(empId, cancellationToken).Result;
    }

    /// <summary>
    /// Retrieves the next appointment for a specific employee.
    /// </summary>
    /// <param name="empId">The ID of the employee.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The next appointment for the specified employee, or null if there are no upcoming appointments.</returns>
    public Appointment? GetNextAppointment(int empId,
            CancellationToken cancellationToken = default)
    {
        return _appointmentRepository.GetNextAppointmentAsync(empId, cancellationToken).Result;
    }

    /// <summary>
    /// Retrieves an appointment by its ID.
    /// </summary>
    /// <param name="id">The ID of the appointment to retrieve.</param> 
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The appointment with the specified ID, or null if not found.</returns>
    public Appointment? GetById(int id, CancellationToken cancellationToken = default)
    {
        return _appointmentRepository.GetByIdAsync(id, cancellationToken).Result;
    }

    /// <summary>
    /// Retrieves all appointments for a specific patient.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An IEnumerable of appointments for the specified patient.</returns>
    public IEnumerable<Appointment> GetAppointmentsByPatientId(int patientId, CancellationToken cancellationToken = default)
    {
        return _appointmentRepository.GetByPatientIdAsync(patientId, cancellationToken).Result;
    }

    /// <summary>
    /// Retrieves a patient by their ID.
    /// </summary>
    /// <param name="patientId">The ID of the patient to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The patient with the specified ID, or null if not found.</returns>
    public Patient? GetPatientById(int patientId, CancellationToken cancellationToken = default)
    {
        return _patientRepository.GetByIdAsync(patientId, cancellationToken).Result;
    }

    /// <summary>
    /// Adds a new appointment to the repository.
    /// </summary>
    /// <param name="appointment">The appointment to add.</param>
    /// <returns>The added appointment, or null if the operation failed.</returns>  
    public Appointment? AddAppointment(Appointment appointment)
    {
        return _appointmentRepository.AddAppointment(appointment).Result;
    }

    /// <summary>
    /// Updates an existing appointment in the repository.
    /// </summary>
    /// <param name="appointment">The appointment to update.</param>
    /// <returns>The updated appointment, or null if the operation failed.</returns>
    public Appointment? UpdateAppointment(Appointment appointment)
    {
        return _appointmentRepository.UpdateAppointment(appointment).Result;
    }

    /// <summary>
    /// Deletes an appointment from the repository.
    /// </summary>
    /// <param name="id">The ID of the appointment to delete.</param>
    /// <returns>The deleted appointment, or null if the operation failed.</returns>
    public Appointment? DeleteAppointment(int id)
    {
        return _appointmentRepository.DeleteAppointment(id).Result;
    }

}