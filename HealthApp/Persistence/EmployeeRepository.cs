
using HealthApp.Model;
using HealthApp.Adapter;

namespace HealthApp.Persistence
{
    /// <summary>
    /// XML-backed repository for employees.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly XmlEmployeeStore _empstore;
        private readonly List<Employee> employees;

        public EmployeeRepository()
        {
            _empstore = new XmlEmployeeStore();
            employees = (List<Employee>)_empstore.GetAllEmployeesAsync().Result;
        }

        public Task<IReadOnlyList<Employee>> GetAllAsync(
        CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyList<Employee>)employees ?? new List<Employee>());

        }

        public Task<IReadOnlyList<Employee>> LoadAllEmployees(
        CancellationToken cancellationToken = default)
        {
            // Implement the logic to load all employee data from XML
            return Task.FromResult((IReadOnlyList<Employee>)employees ?? new List<Employee>());
        }

        public Task<Employee?> GetByIdAsync(
                int id,
                CancellationToken cancellationToken = default)
        {
            return Task.FromResult(employees.Where(a => a.Id == id).FirstOrDefault());
        }

        public Task<Employee> FindByCredentialsAsync(
                string usernameOrEmail,
                string passwordhash,
                CancellationToken cancellationToken = default)
        {
                var employee = employees.FirstOrDefault(e =>
                    (string.Equals(e.Username, usernameOrEmail, StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(e.Email, usernameOrEmail, StringComparison.OrdinalIgnoreCase)) &&
                    e.PasswordHash == passwordhash);
                return Task.FromResult(employee);
           
        }

        public Task<Employee?> AddEmployee(Employee employee)
        {
            _empstore.AddEmployee(employee);
            
            return Task.FromResult(employee);
        }

        public Task<Employee?> UpdateEmployee(Employee employee)
        {
            _empstore.UpdateEmployee(employee);
            return Task.FromResult(employee);
        }

        public Task<Employee?> DeleteEmployee(int id)
        {
            _empstore.DeleteEmployee(id);
            return Task.FromResult((Employee?)null);
        }

        public Task<Employee?> SaveAsync(IEnumerable<Employee> employees)
        {
            _empstore.SaveAsync(employees);
            return Task.FromResult((Employee?)null);
        }

    }
}
