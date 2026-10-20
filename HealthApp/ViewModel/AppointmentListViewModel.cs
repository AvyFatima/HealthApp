using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using HealthApp.Interfaces;
using HealthApp.Model;

namespace HealthApp.ViewModel;

public partial class AppointmentListViewModel : ObservableObject
{
    private readonly IAppointmentService _appointmentservice;
    private readonly INavigationService _navigator;
    public ObservableCollection<Appointment> Appointments { get; private set; }

    public ICommand OnButtonExecutedCommand { get; }

    /// Gets or sets the ID of the appointment list.
    [ObservableProperty]
    private int _listId;


    // Gets or sets the label for the appointment list.
    [ObservableProperty]
    private string _listlbl;

    public AppointmentListViewModel(INavigationService navigator, IAppointmentService appointmentservice)
    {
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        _appointmentservice = appointmentservice;
        OnButtonExecutedCommand = new Command<int>(OnBtnExecuted);
        LoadAppointments();
    }

    /// <summary>
    /// Loads the list of appointments from the appointment service and updates the Appointments collection.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    public void LoadAppointments(CancellationToken cancellationToken = default)
    {
        var appointments = _appointmentservice.GetAllAppointments(GlobalVars.userId);
       
        Appointments = new ObservableCollection<Appointment>(appointments);

        Listlbl = "Appointment List";
    }

    /// <summary>
    /// Handles the execution of the button command, navigating to the details of the selected appointment.
    /// </summary>
    /// <param name="ListId">The ID of the selected appointment.</param>    
    public async void OnBtnExecuted(int ListId)
    {
        await _navigator.GoToAppointmentAsync(ListId);
    }
}