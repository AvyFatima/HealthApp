using HealthApp.Interfaces;
using HealthApp.Model;

namespace HealthApp;

/// <summary>
/// Represents a service for handling user authentication.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IEmployeeRepository _userRepository;
    private readonly IUserSession _userSession;

    public AuthenticationService(IEmployeeRepository userRepository, IUserSession userSession)
    {
        _userRepository = userRepository;
        _userSession = userSession;
    }

    /// <summary>
    /// Asynchronously authenticates a user with the provided username and password.
    /// </summary>
    /// <param name="username">The username of the user to authenticate.</param>
    /// <param name="password">The password of the user to authenticate.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the authenticated Employee
    public async Task<Employee> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.FindByCredentialsAsync(username,password);
        if (user != null)
        {
            _userSession.SignIn(user);
            return user;
        }
        return null;
    }


    /// <summary>
    /// Logs out the currently authenticated user.
    /// </summary>
    public void Logout()
    {
        // Implement logout logic here (e.g., clear session, tokens, etc.)
        _userSession.SignOut();
    }

    /// <summary>
    /// Gets the currently authenticated user.
    /// </summary>
    /// <returns>The currently authenticated Employee, or null if no user is authenticated.</returns>
    public Employee GetAuthenticatedUser()
    {
        // Implement logic to retrieve the currently authenticated user
        // This could involve checking session, tokens, etc.
        return _userSession.CurrentUser;
    }
}