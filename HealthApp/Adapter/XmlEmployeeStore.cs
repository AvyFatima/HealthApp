using HealthApp.DataMap;
using HealthApp.Model;

namespace HealthApp.Adapter;

public class XmlEmployeeStore
{

    private readonly List<Employee> employees;
    // Implement the logic to save and load employee data to/from XML
    private readonly string _filePath;
    // Implement the logic to save and load employee data to/from XML
    private readonly object _lock = new();
    // Implement the logic to save and load employee data to/from XML
    private readonly string _empfilePath = Path.Combine(FileSystem.AppDataDirectory, "Database", "EmpDB.xml");
    // Implement the logic to save and load employee data to/from XML
    private readonly XmlSerializerStore _store;

    public XmlEmployeeStore()
    {
         _store = new XmlSerializerStore(_empfilePath);
        _filePath = _store.FilePath ?? throw new ArgumentException("A file path is required.", nameof(_filePath));
        lock (_lock)
        {
            var employeedb = _store.Load<EmployeeDatabase>() ?? new EmployeeDatabase();
            employees = employeedb.Employees ?? new List<Employee>();
        }
    }

    /// <summary>
    /// Gets all employees asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of all employees.</returns>
    public Task<IReadOnlyList<Employee>> GetAllEmployeesAsync(
        CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            return Task.FromResult((IReadOnlyList<Employee>)employees ?? new List<Employee>());
        }
    }

    public Task<Employee?> AddEmployee(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);
        lock (_lock)
        {
            employees.Add(employee);
            var employee_newdb = new EmployeeDatabase();
            employee_newdb.Employees = employees;
            _store.Save(employee_newdb);
        }
        return Task.FromResult(employee);
    }

    public Task<Employee?> UpdateEmployee(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        lock (_lock)
        {
            var existingEmployee = employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEmployee != null)
            {
                employees.Remove(existingEmployee);

                existingEmployee.Name = employee.Name;
                existingEmployee.Email = employee.Email;

                employees.Add(existingEmployee);

                var employee_newdb = new EmployeeDatabase();
                employee_newdb.Employees = employees;
                _store.Save(employee_newdb);
            }
        }
        return Task.FromResult(employee);
    }

    public Task<Employee?> DeleteEmployee(int id)
    {
        lock (_lock)
        {
            var employeeToRemove = employees.FirstOrDefault(e => e.Id == id);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);

                var employee_newdb = new EmployeeDatabase();
                employee_newdb.Employees = employees;
                _store.Save(employee_newdb);
            }
        }
        return Task.FromResult((Employee?)null);
    }

    public Task<Employee?> SaveAsync(IEnumerable<Employee> employees)
    {
        ArgumentNullException.ThrowIfNull(employees);

        lock (_lock)
        {
            var directory = Path.GetDirectoryName(Path.GetFullPath(_filePath));
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            var employee_newdb = new EmployeeDatabase();
            employee_newdb.Employees = employees.ToList();
            _store.Save(employee_newdb);
        }
        return Task.FromResult((Employee?)null);
    }


}
