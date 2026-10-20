using HealthApp.Model;

namespace HealthApp;

/// <summary>
/// Represents a service for handling user authentication.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates a user with the provided username and password.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>details if authentication is successful; otherwise, null.</returns>
    Task<Employee> AuthenticateAsync(string username, string password);

    /// <summary>
    /// Logs out the currently authenticated user.
    /// </summary>
    void Logout();

    /// <summary>
    /// Gets the currently authenticated user's information.
    /// </summary>
    /// <returns>The authenticated user's information.</returns>
    Employee GetAuthenticatedUser();
   
}