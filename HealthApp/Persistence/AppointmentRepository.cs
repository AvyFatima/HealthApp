using System.Globalization;
using HealthApp.Adapter;
using HealthApp.Model;

namespace HealthApp.Persistence
{

    /// <summary>
    /// Repository for persisting appointments in XML format.
    /// </summary>
    public sealed class AppointmentRepository : IAppointmentRepository
    {
        
        private readonly XmlAppointmentStore _aptstore;
        // Implement the logic to save and load appointment data to/from XML
        private readonly object _lock = new();
        // In-memory list of appointments loaded from the XML database.
        private readonly List<Appointment> appointments;

        public AppointmentRepository()
        {
            _aptstore = new XmlAppointmentStore();
            appointments = (List<Appointment>)_aptstore.GetAllAppointmentsAsync().Result;
        }

        /// <summary>
        /// Retrieves all appointments assigned to a specific employee.
        /// </summary>
        /// <param name="empId">The ID of the employee.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of
        /// appointments assigned to the specified employee.</returns>
        public Task<List<Appointment>> GetAllAsync(int empId, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var allappointments = (List<Appointment>)appointments.Where(a => a.AssignedToId == empId).ToList();
                foreach (var item in allappointments)
                {
                    if (!item.AptnDetails.Contains("with"))
                    {
                        string formattedTime = item.AptnDate.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                        item.AptnDateLabel = formattedTime;
                        var patient = GetPatientByIdAsync(item.AptnPatientId, cancellationToken).Result;
                        item.AptnPatientName = patient?.Name ?? "Unknown patient";
                        item.AptnDetails = item.AptnDetails + " with " + item.AptnPatientName;
                    }

                }
                return Task.FromResult(allappointments);
            }
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the appointment with the specified ID, or null if not found.</returns>
        public Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var appointment = appointments.FirstOrDefault(a => a.AptnId == id);
                if (appointment != null)
                {
                    if (!appointment.AptnDetails.Contains("with"))
                    {
                        string formattednext = appointment.AptnDate.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                        appointment.AptnDateLabel =  formattednext;
                        var patientnext = GetPatientByIdAsync(appointment.AptnPatientId, cancellationToken).Result;
                        appointment.AptnPatientName = patientnext?.Name ?? "Unknown patient";
                        appointment.AptnDetails = $"{appointment.AptnDetails} with {appointment.AptnPatientName}";
                    }
                }
                return Task.FromResult(appointment);
            }
        }

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the patient with the specified ID, or null if not found.</returns>
        public Task<Patient?>  GetPatientByIdAsync(int patientId, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var patientRepository = new PatientRepository();
                return patientRepository.GetByIdAsync(patientId, cancellationToken);
            }
        }

        /// <summary>
        /// Retrieves all appointments for a specific patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of appointments for the specified patient.</returns>
        public Task<List<Appointment>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                    return Task.FromResult(appointments.Where(a => a.AptnPatientId == patientId).ToList());
            }
        }

        /// <summary>
        /// Retrieves all appointments for a specific employee.
        /// </summary>
        /// <param name="empId">The ID of the employee.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of appointments for the specified employee.</returns>
         public Task<List<Appointment>> GetTodaysAppointmentAsync(int empId,
            CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var today = (List<Appointment>)appointments.Where(a => a.AptnDate.Date == DateTime.Today.Date && a.AssignedToId == empId).ToList();
                foreach (var item in today)
                {
                    if (!item.AptnDetails.Contains("with"))
                    {
                        string formattedTime = item.AptnDate.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                        item.AptnDateLabel = formattedTime;
                        var patient = GetPatientByIdAsync(item.AptnPatientId, cancellationToken).Result;
                        item.AptnPatientName = patient?.Name ?? "Unknown patient";
                        item.AptnDetails = item.AptnDetails + " with " + item.AptnPatientName;
                    }

                }
                return Task.FromResult(today);
            }
        }

        /// <summary>
        /// Retrieves the next appointment for a specific employee.
        /// </summary>
        /// <param name="empId">The ID of the employee.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the next appointment for the specified employee, or null if none is found.</returns>
        public Task<Appointment?> GetNextAppointmentAsync(int empId,
            CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var next = appointments.FirstOrDefault(a => a.AptnDate.Date > DateTime.Today.Date && a.AssignedToId == empId);
                if (next != null)
                {
                    if (!next.AptnDetails.Contains("with"))
                    {
                        string formattednext = next.AptnDate.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                        next.AptnDateLabel = next.AptnDate.Date == DateTime.Today.AddDays(1).Date
                            ? "Tomorrow " + formattednext
                            : formattednext;
                        var patientnext = GetPatientByIdAsync(next.AptnPatientId, cancellationToken).Result;
                        next.AptnPatientName = patientnext?.Name ?? "Unknown patient";
                        next.AptnDetails = $"{next.AptnDetails} with {next.AptnPatientName}";
                    }
                }
                return Task.FromResult(next);
            }
        }

        /// <summary>
        /// Adds a new appointment to the repository.
        /// </summary>
        /// <param name="appointment">The appointment to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added appointment.</returns>
        public Task<Appointment?> AddAppointment(Appointment appointment)
        {
            _aptstore.AddAppointment(appointment);
            return Task.FromResult(appointment);
        }

        /// <summary>
        /// Updates an existing appointment in the repository.
        /// </summary>
        /// <param name="appointment">The appointment to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated appointment.</returns>
        public Task<Appointment?> UpdateAppointment(Appointment appointment)
        {
            _aptstore.UpdateAppointment(appointment);
            return Task.FromResult(appointment);
        }

        /// <summary>
        /// Deletes an appointment from the repository.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deleted appointment, or null if not found.</returns>
        public Task<Appointment?> DeleteAppointment(int id)
        {
            _aptstore.DeleteAppointment(id);
            return Task.FromResult((Appointment?)null);
        }

        /// <summary>
        /// Saves the provided appointments to the repository.
        /// </summary>
        /// <param name="appointments">The appointments to save.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved appointment, or null if none is found.</returns>
        public Task<Appointment?> SaveAsync(IEnumerable<Appointment> appointments)
        {
            _aptstore.SaveAsync(appointments);
            return Task.FromResult((Appointment?)null);
        }
    }
}