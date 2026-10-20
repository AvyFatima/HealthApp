using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using HealthApp.Interfaces;
using HealthApp.Model;
using HealthApp.FrontendDTO;
using System.Collections.ObjectModel;

namespace HealthApp.ViewModel
{
	public partial class HomePageViewModel : ObservableObject
    {
        private readonly IHomeDashboardService _dashboard;
        private readonly INavigationService _navigator;
        private readonly IUserSession _userSession;

        // Gets or sets the name of the employee.
        [ObservableProperty]
        private string empName = string.Empty;

        // Gets or sets the designation of the employee.
        [ObservableProperty]
        private string empDesig = string.Empty;

        // Gets or sets a value indicating whether the next appointment button is enabled.
        [ObservableProperty]
        private bool nextApntBtnEnabled;

        [ObservableProperty]
        private Appointment nextAppointment;

        public ObservableCollection<AppointmentRow> Appointments { get; } = new();

        public ICommand SettingsCommand { get; }
        public ICommand PlayCommand { get; }
        public ICommand UserCommand { get; }
        public ICommand HomeCommand { get; }
        public ICommand VideoCommand { get; }

        public ICommand PatientsCommand { get; }
        public ICommand ApntsCommand { get; }
        public ICommand MsgCommand { get; }
        public ICommand ReportCommand { get; }

        public ICommand NextApntCommand { get; }

        [ObservableProperty]
        private IReadOnlyList<Appointment> todaysAppointments =   Array.Empty<Appointment>();

        public HomePageViewModel(IHomeDashboardService dashboard, INavigationService navigator,
            IUserSession userSession)
        {
            _dashboard = dashboard ?? throw new ArgumentNullException(nameof(dashboard));
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
            _userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));

            SettingsCommand = new Command(OnSettingsExecuted);
            PlayCommand = new Command(OnPlayExecuted);
            UserCommand = new Command(OnUserExecuted);
            HomeCommand = new Command(OnHomeExecuted);
            VideoCommand = new Command(OnVideoExecuted);

            PatientsCommand = new Command(OnPatientsExecuted);
            ApntsCommand = new Command(OnApntsExecuted);
            MsgCommand = new Command(OnMsgExecuted);
            ReportCommand = new Command(OnReportExecuted);

            NextApntCommand = new Command(OnNextApntExecuted);
        }

        /// <summary>
        /// Loads the dashboard data for the specified employee ID.
        /// </summary>  
        /// <param name="employeeId">The ID of the employee for whom to load the dashboard data.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task LoadAsync(
            int employeeId,
            CancellationToken cancellationToken = default)
        {
            var result = await _dashboard.GetDashboardAsync(
                employeeId,
                cancellationToken);

            TodaysAppointments = result.TodaysAppointments;
            NextAppointment = result.NextAppointment;
            EmpName = result.Employee.Name;
            EmpDesig = result.Employee.Role;
            if (EmpDesig == "Doctor")
                EmpDesig = "Dr.";

           
            Appointments.Clear();
            foreach (var appointment in TodaysAppointments)
            {
                Appointments.Add(new AppointmentRow(
                    appointment.AptnDate.ToString("hh:mm tt", CultureInfo.InvariantCulture),
                    " "+appointment.AptnDetails ?? string.Empty));
            }

            if (NextAppointment is not null &&
                NextAppointment.AptnDate.Date == DateTime.Today.AddDays(1).Date)
            {
                NextApntBtnEnabled = true;
            }
        }


        /// <summary>
        /// Handles the execution of the settings command, signing out the user and navigating to the login page.
        /// </summary>
        private async void OnSettingsExecuted()
        {
            _userSession.SignOut();

            await _navigator.GoToLoginAsync();
        }

        /// <summary>
        /// Handles the execution of the next appointment command, navigating to the details of the next appointment.
        /// </summary>
        private async void OnNextApntExecuted()
        {
            if (NextAppointment is not null)
                await _navigator.GoToAppointmentAsync(NextAppointment.AptnId);
        }

        /// <summary>
        /// Handles the execution of the play command.
        /// </summary>
        private void OnPlayExecuted()
        {}

        private void OnUserExecuted()
        {}

        private void OnHomeExecuted()
        {}

        private void OnVideoExecuted()
        {}

        /// <summary>       
        /// Handles the execution of the patients command, navigating to the list of patients.
        /// </summary>
        private async void OnPatientsExecuted()
        {
            await _navigator.GoToListAsync("Person");
        }

        /// <summary>
        /// Handles the execution of the appointments command, navigating to the list of appointments.
        /// </summary>
        private async void OnApntsExecuted()
        {
            await _navigator.GoToListAsync("Appointment");
        }

        /// <summary>
        /// Handles the execution of the messages command, navigating to the list of messages.
        /// </summary>
        private void OnMsgExecuted()
        { }

        /// <summary>
        /// Handles the execution of the reports command, navigating to the list of reports.
        /// </summary>
        private void OnReportExecuted()
        { }


    }
}
