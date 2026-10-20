namespace HealthApp.Interfaces
{
    public interface IUserMessageService
    {
        Task ShowMessageAsync(string title, string message, string cancel);
        Task ShowAlertAsync(string title, string message);
        Task<bool> ShowConfirmationAsync(string title, string message);
        Task<string> ShowTextInputAsync(string title, string message, string placeholder);
   }
}
