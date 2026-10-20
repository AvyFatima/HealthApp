using HealthApp.DataMap;
using HealthApp.Model;

namespace HealthApp.Adapter
{
	public class XmlPersonStore
	{
        private readonly List<Patient> patients;
        // Implement the logic to save and load Patient data to/from XML
        private readonly string _filePath;
        // Implement the logic to save and load Patient data to/from XML
        private readonly object _lock = new();
        // Implement the logic to save and load Patient data to/from XML
        private readonly string _ptfilePath = Path.Combine(FileSystem.AppDataDirectory, "Database", "PatientsDB.xml");
        // Implement the logic to save and load Patient data to/from XML
        private readonly XmlSerializerStore _store;

        public XmlPersonStore()
        {
            _store = new XmlSerializerStore(_ptfilePath);
            _filePath = _store.FilePath ?? throw new ArgumentException("A file path is required.", nameof(_filePath));
            lock (_lock)
            {
                var patientdb = _store.Load<PatientDatabase>() ?? new PatientDatabase();
                patients = patientdb.Patients ?? new List<Patient>();
            }
        }


        /// <summary>
        /// Gets all patients asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of all patients.</returns>
        public Task<IReadOnlyList<Patient>> GetAllPatientsAsync(
            CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                return Task.FromResult((IReadOnlyList<Patient>)patients ?? new List<Patient>());
            }
        }


        public Task<Patient?> AddPatient(Patient patient)
        {
            ArgumentNullException.ThrowIfNull(patient);
            lock (_lock)
            {
                patients.Add(patient);
                var patient_newdb = new PatientDatabase();
                patient_newdb.Patients = patients;
                _store.Save(patient_newdb);
            }
            return Task.FromResult(patient);
        }

        public Task<Patient?> UpdatePatient(Patient patient)
        {
            ArgumentNullException.ThrowIfNull(patient);

            lock (_lock)
            {
                var existingPatient = patients.FirstOrDefault(e => e.Id == patient.Id);
                if (existingPatient != null)
                {
                    patients.Remove(existingPatient);

                    existingPatient.Name = patient.Name;
                    existingPatient.Address = patient.Address;
                    existingPatient.Age = patient.Age;
                    existingPatient.BloodGroup = patient.BloodGroup;

                    patients.Add(existingPatient);

                    var patient_newdb = new PatientDatabase();
                    patient_newdb.Patients = patients;
                    _store.Save(patient_newdb);
                }
            }
            return Task.FromResult(patient);
        }

        public Task<Patient?> DeletePatient(int id)
        {
            lock (_lock)
            {
                var patientToRemove = patients.FirstOrDefault(e => e.Id == id);
                if (patientToRemove != null)
                {
                    patients.Remove(patientToRemove);

                    var patient_newdb = new PatientDatabase();
                    patient_newdb.Patients = patients;
                    _store.Save(patient_newdb);
                }
            }
            return Task.FromResult((Patient?)null);
        }

        public Task<Patient?> SaveAsync(IEnumerable<Patient> patients)
        {
            ArgumentNullException.ThrowIfNull(patients);

            lock (_lock)
            {
                var directory = Path.GetDirectoryName(Path.GetFullPath(_filePath));
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);

                var patient_newdb = new PatientDatabase();
                patient_newdb.Patients = patients.ToList();
                _store.Save(patient_newdb);
            }
            return Task.FromResult((Patient?)null);
        }




    }
}

