using HealthApp.Interfaces;
using HealthApp.Model;

namespace HealthApp.Services;

public sealed class UserSession : IUserSession
{
	public Employee? CurrentUser { get; private set; }

	/// <summary>
	/// Signs in the specified user and sets the current user in the session.
	/// </summary>
	/// <param name="user">The employee to sign in.</param>
	public void SignIn(Employee user)
	{
		CurrentUser = user ?? throw new ArgumentNullException(nameof(user));
		GlobalVars.userId = user.Id;
	}

	/// <summary>
	/// Signs out the current user and clears the session.
	/// </summary>
	public void SignOut()
	{
		CurrentUser = null;
		GlobalVars.userId = 0;
	}
}
