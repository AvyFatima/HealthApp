using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using HealthApp.DataMap;
using HealthApp.Interfaces;
using HealthApp.Model;

namespace HealthApp.ViewModel
{
    [QueryProperty(nameof(PatientId), "PersonId")]
    public partial class PersonDetailViewModel : ObservableObject
    {

        private readonly IPatientService _patientservice;
        private readonly IAppointmentService _appointmentservice;
        private readonly INavigationService _navigator;
        public Patient PatientDetail { get; set; }

        public ObservableCollection<Appointment> PatientApnts { get; set; }

        [ObservableProperty]
        private string _ptnDetails;

        [ObservableProperty]
        private string _patientName;

        [ObservableProperty]
        private string _patientCity;

        [ObservableProperty]
        private string _ptnAge;

        [ObservableProperty]
        private string _ptnBlood;


        [ObservableProperty]
        private int _personId;

        public int PatientId
        {
            get => _personId;
            set
            {
                _personId = value;
                if (_personId != 0)
                    GetPersonDetails(_personId); // Trigger data load automatically when the ID arrives
            }
        }

        public PersonDetailViewModel(INavigationService navigator, IPatientService patientservice, IAppointmentService appointmentservice)
        {
            _appointmentservice = appointmentservice;
            _patientservice = patientservice;
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        }

        /// <summary>
        /// Retrieves the details of a patient and their appointments based on the provided ID.
        /// </summary>  
        /// <param name="Id">The ID of the patient whose details are to be retrieved.</param>
        private void GetPersonDetails(int Id)
        {
            try
            {
                PatientDetail = _patientservice.GetPatientById(Id);

                var appointments = _appointmentservice.GetAppointmentsByPatientId(Id);
                PatientApnts = new ObservableCollection<Appointment>(appointments);
                PatientName = PatientDetail.Name;
                PtnAge = "Age: " + PatientDetail.Age.ToString();
                PtnBlood = "Blood Group: " +  PatientDetail.BloodGroup;
                PatientCity = "";

                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation failed: {ex.Message}");
            }
        }

        
    }
}

