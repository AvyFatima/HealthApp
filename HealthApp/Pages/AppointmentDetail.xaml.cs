using System.ComponentModel;
using HealthApp.ViewModel;

namespace HealthApp;

public partial class AppointmentDetail : ContentPage
{
    private AppointmentDetailViewModel _AppointmentDetailViewModel;
    

    public AppointmentDetail(AppointmentDetailViewModel viewModel)
	{
		InitializeComponent();
        _AppointmentDetailViewModel = viewModel;
        BindingContext = _AppointmentDetailViewModel;
        _AppointmentDetailViewModel.PropertyChanged += OnViewModelPropertyChanged;
        BindNotesRemindersGrid();
    }

    /// <summary>
    /// Binds the notes and reminders from the appointment detail to the respective grids.
    /// </summary>
    private void BindNotesRemindersGrid()
    {
        if (_AppointmentDetailViewModel.AppointmentDetail != null)
        {
            //Notes
            NotesGrid.RowDefinitions.Clear();

            if (_AppointmentDetailViewModel.AppointmentDetail.Notes != null)
            {
                foreach (var item in _AppointmentDetailViewModel.AppointmentDetail.Notes)
                {
                    if (item != null)
                    {
                        // Add a new row for each note and set its height to auto
                        int i = NotesGrid.RowDefinitions.Count;
                        NotesGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        var notelbl = new Label { Text = item, FontAttributes = FontAttributes.Bold, FontSize = 14, TextColor = Colors.Black };
                        Grid.SetRow(notelbl, i);

                        NotesGrid.Children.Add(notelbl);
                    }
                }
            }


            //Reminders

            RemindersGrid.RowDefinitions.Clear();
            if (_AppointmentDetailViewModel.AppointmentDetail.Reminders != null)
            {
                foreach (var item in _AppointmentDetailViewModel.AppointmentDetail.Reminders)
                {
                    if (item != null)
                    {
                        int i = RemindersGrid.RowDefinitions.Count;
                        RemindersGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        var remlbl = new Label { Text = item, FontAttributes = FontAttributes.Bold, FontSize = 14, TextColor = Colors.Black };
                        Grid.SetRow(remlbl, i);

                        RemindersGrid.Children.Add(remlbl);
                    }
                }
            }
        }
    }

    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // If the property that changed is AptnDetails, we want to rebind the notes and 
        // reminders grid to reflect any changes in the appointment details.
        if (e.PropertyName == nameof(AppointmentDetailViewModel.AptnDetails))
        {
            BindNotesRemindersGrid();
        }
    }


}
