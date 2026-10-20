using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HealthApp.ViewModel;

namespace HealthApp;

[QueryProperty(nameof(RecordType), "Listtype")]
public partial class ListRecords : ContentPage
{
    private AppointmentListViewModel _AppointmentListViewModel;
    private PatientListViewModel _PatientListViewModel;

    // Initializes a new instance of the ListRecords page with the specified appointment and patient list view models.
    private string Listtype;

    /// <summary>
    /// Gets or sets the type of records to display (either "Person" or "Appointment").
    /// When set, it triggers the binding of the records grid to reflect the selected type.
    /// </summary>
    public string RecordType
    {
        get => Listtype;
        set
        {
            Listtype = value;
            BindRecordsGrid();
        }
    }

    public ListRecords(AppointmentListViewModel appointmentListViewM, PatientListViewModel patientListViewM )
	{
		InitializeComponent();

        // Initialize the ViewModels and set up property change notifications
        _PatientListViewModel = patientListViewM;
        _PatientListViewModel.PropertyChanged += OnViewModelPropertyChanged;

        _AppointmentListViewModel = appointmentListViewM;
        _AppointmentListViewModel.PropertyChanged += OnViewModelPropertyChanged;
        
        // Bind the view model to the page based on the current list type (Person or Appointment)
        if (Listtype == "Person")
        { 
            BindingContext = _PatientListViewModel;
        }
        else
        {
            BindingContext = _AppointmentListViewModel;
        }
    }

    /// <summary>
    /// Binds the records grid based on the current list type (Person or Appointment).
    /// </summary>
    private void BindRecordsGrid()
    {
        //Records
        
        if (Listtype == "Person")
        {
            BindingContext = _PatientListViewModel;

            RecordsGrid.RowDefinitions.Clear();
            if (_PatientListViewModel.Patients != null)
            {
                foreach (var item in _PatientListViewModel.Patients)
                {
                    if (item != null)
                    {
                        LoadGrid(item.Name, item.Id);
                    }
                }
            }
        }
        else
        {
            if (_AppointmentListViewModel.Appointments != null)
            {
                RecordsGrid.RowDefinitions.Clear();
                foreach (var item in _AppointmentListViewModel.Appointments)
                {
                    if (item != null)
                    {
                        LoadGrid(item.AptnDetails, item.AptnId);
                        
                    }
                }
            }
        }
    }

    /// <summary>
    /// Loads a record into the grid with the specified details and ID.
    /// </summary>  
    /// <param name="Details">The details of the record to display.</param>
    /// <param name="Id">The ID of the record, used for command parameter binding
    private void LoadGrid(string Details, int Id)
    {
        int i = RecordsGrid.RowDefinitions.Count;
        RecordsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        var itemlbl = new Label { Text = Details, FontAttributes = FontAttributes.Bold, FontSize = 14, TextColor = Colors.Black, HorizontalOptions = LayoutOptions.Start };
        Grid.SetColumn(itemlbl, 0);
        Grid.SetRow(itemlbl, i);

        var itembtn = new Button { Text = " View Details", FontAttributes = FontAttributes.Bold, FontSize = 14, HorizontalOptions = LayoutOptions.End };

        itembtn.SetBinding(Button.CommandProperty, new Binding(nameof(_AppointmentListViewModel.OnButtonExecutedCommand)));
        itembtn.CommandParameter = Id;

        Grid.SetColumn(itembtn, 1);
        Grid.SetRow(itembtn, i);

        RecordsGrid.Children.Add(itemlbl);
        RecordsGrid.Children.Add(itembtn);
    }

    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // If the property that changed is Listtype, we want to rebind the records grid to reflect any changes in the list type.
        if (e.PropertyName == nameof(Listtype))
        {
            BindRecordsGrid();
        }
    }
}
