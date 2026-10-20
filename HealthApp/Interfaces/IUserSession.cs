
using HealthApp.Model;

namespace HealthApp.Interfaces;

public interface IUserSession
{
    Employee? CurrentUser { get; }
    void SignIn(Employee user);
    void SignOut();
}
