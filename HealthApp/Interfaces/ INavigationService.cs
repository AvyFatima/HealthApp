namespace HealthApp;

/// <summary>
/// Represents a service for handling navigation within the application.
/// </summary>
public interface INavigationService
{
    Task GoToHomeAsync();
    Task GoToPatientAsync(int patientId);
    Task GoToAppointmentAsync(int appointmentId);
    Task GoToListAsync(string recordType);
    Task GoToLoginAsync();

}