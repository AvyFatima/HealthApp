using HealthApp.Model;

namespace HealthApp;

/// <summary>
/// Represents a repository for managing patient data.
/// </summary>
public interface IPatientRepository
{
        Task<IReadOnlyList<Patient>> GetAllAsync(
        CancellationToken cancellationToken = default);

        Task<Patient?> GetByIdAsync(
                int id,
                CancellationToken cancellationToken = default);

        Task<Patient?> AddPatient(Patient patient);
        Task<Patient?> UpdatePatient(Patient patient);
        Task<Patient?> DeletePatient(int id);
        Task<Patient?> SaveAsync(IEnumerable<Patient> patients);
}
