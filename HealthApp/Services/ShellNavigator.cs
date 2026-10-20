
namespace HealthApp.Services;

public sealed class ShellNavigator : INavigationService
{
     public Task GoToHomeAsync()
    {
        return Shell.Current.GoToAsync("HomePage");
    }

    public Task GoToPatientAsync(int patientId)
    {
        var parameters = new Dictionary<string, object>
        {
            ["PersonId"] = patientId
        };

        return Shell.Current.GoToAsync("PersonDetails", parameters);
    }

    public Task GoToAppointmentAsync(int appointmentId)
    {
        var parameters = new Dictionary<string, object>
        {
            ["AptnId"] = appointmentId
        };

        return Shell.Current.GoToAsync("AppointmentDetail", parameters);
    }

    public Task GoToLoginAsync()
    {
        return Shell.Current.GoToAsync("LoginPage");
    }

    public Task GoToListAsync(string recordType)
    {
        var parameters = new Dictionary<string, object>
        {
            ["Listtype"] = recordType
        };
        return Shell.Current.GoToAsync("ListRecords", parameters);
    }

}
