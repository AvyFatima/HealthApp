using System;
using System.Globalization;
using HealthApp.Interfaces;
using HealthApp.Model;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace HealthApp.ViewModel
{

    [QueryProperty(nameof(AppointmentId), "AptnId")]
    public partial class AppointmentDetailViewModel : ObservableObject
    {
        private readonly IAppointmentService _appointmentservice;
        private readonly INavigationService _navigator;
        public Appointment AppointmentDetail { get; set; }

        public ICommand PatientCommand { get;  }

        public bool PatientDetailsBtnEnabled { get; set; }

        [ObservableProperty]
        private string _aptnDetails;

        [ObservableProperty]
        private string _patientName;

        [ObservableProperty]
        private int _patientId;

        [ObservableProperty]
        private string _aptnDate;

        [ObservableProperty]
        private string _aptnTime;


        [ObservableProperty]
        private int _aptnId;

        public int AppointmentId
        {
            get => _aptnId;
            set
            {
                _aptnId = value;
                if (_aptnId != 0)
                    GetAppointmentDetailsAsync(_aptnId); // Trigger data load automatically when the ID arrives
            }
        }

        public AppointmentDetailViewModel(INavigationService navigator, IAppointmentService appointmentservice)
        {
            _appointmentservice = appointmentservice;
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

            PatientDetailsBtnEnabled = false;
            PatientCommand = new Command(OnPatientExecuted);
            if (AppointmentId != 0)
            {
                GetAppointmentDetailsAsync(AppointmentId);
                
            }

        }

        private void GetAppointmentDetailsAsync(int Id)
        {
            if (Id != 0)
            {
                PatientDetailsBtnEnabled = true;
                AppointmentDetail = _appointmentservice.GetById(Id);
                if (AppointmentDetail is null)
                    return;

                PatientId = AppointmentDetail.AptnPatientId;
                PatientName = _appointmentservice.GetPatientById(AppointmentDetail.AptnPatientId)?.Name
                    ?? "Unknown patient";
                AptnDetails = AppointmentDetail.AptnDetails;

                AptnDate = "Date: " + AppointmentDetail.AptnDate.Date.ToString("MMM dd,yyyy");
                AptnTime = "Time: " + AppointmentDetail.AptnDate.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            }

        }

        private async void OnPatientExecuted()
        {
            try
            {
                await _navigator.GoToPatientAsync(PatientId);
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine($"Navigation failed: {ex.Message}");
            }

        }

    }
}
