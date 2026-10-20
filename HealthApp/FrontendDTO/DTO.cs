namespace HealthApp.FrontendDTO;

public sealed record AppointmentListItem(
    int Id,
    string Time,
    string Description,
    string PatientName);

public sealed record AppointmentRow(
    string Time,
    string Description);

public sealed record PatientListItem(
    int Id,
    string Name,
    string DateOfBirth,
    string Gender);


public sealed record UserListItem(
    int Id,
    string Username,
    string Role);

public sealed record EmployeeListItem(
    int Id,
    string Name,
    string Role);
    
