using HealthApp.DataMap;
using HealthApp.Model;

namespace HealthApp.Adapter;

public class XmlAppointmentStore
{

    private readonly List<Appointment> appointments;
    // Implement the logic to save and load appointment data to/from XML
    private readonly string _filePath;
    // Implement the logic to save and load appointment data to/from XML
    private readonly object _lock = new();
    // Implement the logic to save and load appointment data to/from XML
    private readonly string _aptfilePath = Path.Combine(FileSystem.AppDataDirectory, "Database", "AppointmentsDB.xml");
    // Implement the logic to save and load appointment data to/from XML
    private readonly XmlSerializerStore _store;

    public XmlAppointmentStore()
    {
         _store = new XmlSerializerStore(_aptfilePath);
        _filePath = _store.FilePath ?? throw new ArgumentException("A file path is required.", nameof(_filePath));
        lock (_lock)
        {
            var appointmentdb = _store.Load<AppointmentDatabase>() ?? new AppointmentDatabase();
            appointments = appointmentdb.Appointments ?? new List<Appointment>();
        }
    }

    /// <summary>
    /// Gets all appointments asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of all appointments.</returns>
    public Task<IReadOnlyList<Appointment>> GetAllAppointmentsAsync(
        CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            return Task.FromResult((IReadOnlyList<Appointment>)appointments ?? new List<Appointment>());
        }
    }


    public Task<Appointment?> AddAppointment(Appointment appointment)
    {
        ArgumentNullException.ThrowIfNull(appointment);
        lock (_lock)
        {
            appointments.Add(appointment);
            var appointment_newdb = new AppointmentDatabase();
            appointment_newdb.Appointments = appointments;
            _store.Save(appointment_newdb);
        }
        return Task.FromResult(appointment);
    }

    public Task<Appointment?> UpdateAppointment(Appointment appointment)
    {
        ArgumentNullException.ThrowIfNull(appointment);

        lock (_lock)
        {
            var existingappointment = appointments.FirstOrDefault(e => e.AptnId == appointment.AptnId);
            if (existingappointment != null)
            {
                appointments.Remove(existingappointment);

                existingappointment.AptnDetails = appointment.AptnDetails;
                existingappointment.AptnDate = appointment.AptnDate;

                appointments.Add(existingappointment);

                var appointment_newdb = new AppointmentDatabase();
                appointment_newdb.Appointments = appointments;
                _store.Save(appointment_newdb);
            }
        }
        return Task.FromResult(appointment);
    }

    public Task<Appointment?> DeleteAppointment(int id)
    {
        lock (_lock)
        {
            var appointmentToRemove = appointments.FirstOrDefault(e => e.AptnId == id);
            if (appointmentToRemove != null)
            {
                appointments.Remove(appointmentToRemove);

                var appointment_newdb = new AppointmentDatabase();
                appointment_newdb.Appointments = appointments;
                _store.Save(appointment_newdb);
            }
        }
        return Task.FromResult((Appointment?)null);
    }

    public Task<Appointment?> SaveAsync(IEnumerable<Appointment> appointments)
    {
        ArgumentNullException.ThrowIfNull(appointments);

        lock (_lock)
        {
            var directory = Path.GetDirectoryName(Path.GetFullPath(_filePath));
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            var appointment_newdb = new AppointmentDatabase();
            appointment_newdb.Appointments = appointments.ToList();
            _store.Save(appointment_newdb);
        }
        return Task.FromResult((Appointment?)null);
    }


}
