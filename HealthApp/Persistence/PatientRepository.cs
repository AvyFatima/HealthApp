
using HealthApp.Adapter;
using HealthApp.Model;

namespace HealthApp.Persistence
{
    
public sealed class PatientRepository : IPatientRepository
{
        private readonly XmlPersonStore _ptstore;
        private readonly List<Patient> patients;


	//	 Initializes a new instance of the PatientRepository class for storing patient data.
	public PatientRepository()
	{
         _ptstore = new XmlPersonStore();
         patients = (List<Patient>)_ptstore.GetAllPatientsAsync().Result;
        
    }

	///	 Asynchronously retrieves all patients from the repository.
	///	 <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
	///	 <returns>A task that represents the asynchronous operation. The
	public Task<IReadOnlyList<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
	{
            return Task.FromResult((IReadOnlyList<Patient>)patients ?? new List<Patient>());
    }
	
	///	 Asynchronously retrieves a patient by their ID from the repository.
	///	 <param name="id">The ID of the patient to retrieve.</param>
	///	 <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
	///	 <returns>A task that represents the asynchronous operation. The
	public Task<Patient?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
            return Task.FromResult(patients.Where(a => a.Id == id).FirstOrDefault());
        }

	///	 Asynchronously adds a new patient to the repository.
	///	 <param name="patient">The patient to add.</param>
	///	 <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
	///	 <returns>A task that represents the asynchronous operation. The
	public Task<Patient?> AddPatient(Patient patient)
	{
            _ptstore.AddPatient(patient);
            return Task.FromResult(patient);
	}

	///	 Asynchronously updates an existing patient in the repository.
	///	 <param name="patient">The patient to update.</param>
	///	 <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
	///	 <returns>A task that represents the asynchronous operation. The
	public Task<Patient?> UpdatePatient(Patient patient)
	{
            _ptstore.UpdatePatient(patient);
            return Task.FromResult(patient);
	}

	///	 Asynchronously deletes a patient by their ID from the repository.
	///	 <param name="id">The ID of the patient to delete.</param>	
	/// 	<param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
	///	 <returns>A task that represents the asynchronous operation. The
	public Task<Patient?> DeletePatient(int id)
	{
            _ptstore.DeletePatient(id);
            return Task.FromResult((Patient?)null);
	}

	///	 Asynchronously saves a collection of patients to the repository.
	///	 <param name="patients">The collection of patients to save.</param>
	///	 <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
	///	 <returns>A task that represents the asynchronous operation. </returns>
	public Task<Patient?> SaveAsync(IEnumerable<Patient> patients)
	{
            _ptstore.SaveAsync(patients);
            return Task.FromResult((Patient?)null);
	}
}
}