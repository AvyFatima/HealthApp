using HealthApp.Interfaces;

namespace HealthApp.Services;

public sealed class MauiUserMessageService : IUserMessageService
{
    /// <summary>
    /// Displays a message to the user with a title, message, and an optional cancel button.
    /// </summary>
    /// <param name="title">The title of the message.</param>
    /// <param name="message">The message content.</param>
    /// <param name="cancel">The text for the cancel button (default is "OK").</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
	public Task ShowMessageAsync(string title, string message, string cancel = "OK")
	{
		return MainThread.InvokeOnMainThreadAsync(() =>
			Shell.Current.DisplayAlert(title, message, cancel));
	}

    /// <summary>
    /// Displays a confirmation dialog to the user with a title, message, and options for confirmation and cancellation.
    /// </summary>
    /// <param name="title">The title of the confirmation dialog.</param>
    /// <param name="message">The message content of the confirmation dialog.</param>
    /// <param name="accept">The text for the accept button (default is "Yes").</param>
    /// <param name="cancel">The text for the cancel button (default is "No").</param>
    /// <returns>A task that represents the asynchronous operation, containing the user's choice.</returns>
    public Task ShowAlertAsync(string title, string message)
    {
        return MainThread.InvokeOnMainThreadAsync(() =>
            Shell.Current.DisplayAlert(title, message, "OK", "Cancel"));
    }

    /// <summary>
    /// Displays a confirmation dialog to the user with a title, message, and options for confirmation and cancellation.
    /// </summary>
    /// <param name="title">The title of the confirmation dialog.</param>
    /// <param name="message">The message content of the confirmation dialog.</param>
    /// <returns>A task that represents the asynchronous operation, containing the user's choice.</returns>         
    public Task<bool> ShowConfirmationAsync(string title, string message)
    {
        return MainThread.InvokeOnMainThreadAsync(() =>
            Shell.Current.DisplayAlert(title, message, "Yes", "No"));
    }

/// <summary>
    /// Displays a text input dialog to the user with a title, message, and placeholder text.
    /// </summary>      
    /// <param name="title">The title of the text input dialog.</param>
    /// <param name="message">The message content of the text input dialog.</param>
    /// <param name="placeholder">The placeholder text for the input field.</param>
    /// <returns>A task that represents the asynchronous operation, containing the user's input.</returns>
    public Task<string> ShowTextInputAsync(string title, string message, string placeholder)
    {
        return MainThread.InvokeOnMainThreadAsync(() =>
            Shell.Current.DisplayPromptAsync(title, message, "OK", "Cancel", placeholder));
    }
}
