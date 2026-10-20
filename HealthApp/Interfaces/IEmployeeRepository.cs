using HealthApp.Model;

namespace HealthApp;

/// <summary>
/// Represents a repository for managing employee data.
/// </summary>
public interface IEmployeeRepository
{
        Task<IReadOnlyList<Employee>> GetAllAsync(
        CancellationToken cancellationToken = default);

        Task<Employee?> GetByIdAsync(
                int id,
                CancellationToken cancellationToken = default);

        Task<Employee?> FindByCredentialsAsync(
                string usernameOrEmail,
                string password,
                CancellationToken cancellationToken = default);


        Task<Employee?> AddEmployee(Employee employee);
        Task<Employee?> UpdateEmployee(Employee employee);
        Task<Employee?> DeleteEmployee(int id);
}

