using HealthApp.Model;

namespace HealthApp.Interfaces;

public interface IPatientService
{
    Patient? GetPatientById(int id);
    IEnumerable<Patient> GetAllPatients();
    void AddPatient(Patient patient);
    void UpdatePatient(Patient patient);
    void DeletePatient(int id);
}
