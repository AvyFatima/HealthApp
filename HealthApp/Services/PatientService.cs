using HealthApp.Interfaces;
using HealthApp.Model;

namespace HealthApp;

/// <summary>
/// Represents a repository for managing patient data.
/// </summary>
public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    /// <summary>
    /// Retrieves a patient by their ID.
    /// </summary>
    /// <param name="id">The ID of the patient to retrieve.</param>
    /// <returns>The patient with the specified ID, or null if not found.</returns>
    public Patient GetPatientById(int id)
    {
        return _patientRepository.GetByIdAsync(id).Result;
    }

    /// <summary>
    /// Retrieves all patients.
    /// </summary>
    /// <returns>An IEnumerable of all patients.</returns>
    public IEnumerable<Patient> GetAllPatients()
    {
        return _patientRepository.GetAllAsync().Result;
    }

    /// <summary>
    /// Adds a new patient.
    /// </summary>
    /// <param name="patient">The patient to add.</param>
    public void AddPatient(Patient patient)
    {
        _patientRepository.AddPatient(patient);
    }

    /// <summary>
    /// Updates an existing patient.
    /// </summary>
    /// <param name="patient">The patient to update.</param>
    public void UpdatePatient(Patient patient)
    {
        _patientRepository.UpdatePatient(patient);
    }

    /// <summary>
    /// Deletes a patient by their ID.
    /// </summary>
    /// <param name="id">The ID of the patient to delete.</param>
    public void DeletePatient(int id)
    {
        _patientRepository.DeletePatient(id);
    }
}