using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using HealthApp.Model;

namespace HealthApp.ViewModel
{
    public partial class PatientListViewModel : ObservableObject
    {
        private readonly IPatientRepository _patients;
        private readonly INavigationService _navigator;
        public ObservableCollection<Patient> Patients { get; private set; }

        /// <summary>
        /// The command that is executed when the button is clicked.
        /// </summary>
        public ICommand OnButtonExecutedCommand { get; }

        /// Gets or sets the ID of the patient list.
        [ObservableProperty]
        private int _listId;

        /// Gets or sets the label for the patient list.
        [ObservableProperty]
        private string _listlbl;

        public PatientListViewModel(INavigationService navigator, IPatientRepository patients)
        {
            _patients = patients;
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
            OnButtonExecutedCommand = new Command<int>(OnBtnExecuted);
            LoadPatientsAsync();
        }

        /// <summary>
        /// Asynchronously loads the list of patients from the repository and updates the Patients collection.
        /// </summary>
        public async Task LoadPatientsAsync()
        {
            var patients = await _patients.GetAllAsync();
            Patients = new ObservableCollection<Patient>(patients);
            Listlbl = "Patient List";
        }

        /// <summary>
        /// Handles the execution of the button command, navigating to the details of the selected patient.
        /// </summary>  
        /// <param name="ListId">The ID of the selected patient.</param>
        public async void OnBtnExecuted(int ListId)
        {
            await _navigator.GoToPatientAsync(ListId);
        }


    }
}