using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthApp.Model;
using HealthApp.Interfaces;

namespace HealthApp.ViewModel
{
	public class LoginPageViewModel : ObservableObject
    {
        private readonly IAuthenticationService _authentication;
        private readonly INavigationService _navigator;
        private readonly IUserMessageService _messages;

        public LoginPageViewModel(IAuthenticationService authentication, INavigationService navigator, IUserMessageService messages)
        {
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            LoginCommand = new Command(async () => await LoginAsync());
            ForgetPasswordCommand = new Command(OnForgetPasswordExecuted);
            Username = "admin";
            Password = "password";
        }

        private string _username ;
        private string _password;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }
        
        public ICommand LoginCommand { get; }

        public ICommand ForgetPasswordCommand { get; }

        private async Task LoginAsync()
        {
            try
            {
                // Simple validation check
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    await _messages.ShowAlertAsync("Error", "Please fill in all fields.");
                    return;
                }

                var employee = _authentication.AuthenticateAsync(Username, Password);

                if (employee is null || GlobalVars.userId == 0)
                {
                    await _messages.ShowAlertAsync("Error", "Invalid credentials.");
                }
                await _navigator.GoToHomeAsync();
            }
            catch(Exception ex)
            {
                await _messages.ShowAlertAsync("Error", ex.Message);
            }
        }


        private void OnForgetPasswordExecuted()
        {
            //Just for testing purpose, in real application you would navigate to a password recovery page or show a dialog
           // _messages.ShowInfoAsync("Password recovery is not implemented yet.");
            Username = "admin";
            Password = "password";
        }

    }
}
