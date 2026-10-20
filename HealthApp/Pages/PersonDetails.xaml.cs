using System.ComponentModel;
using HealthApp.ViewModel;

namespace HealthApp;

public partial class PersonDetails : ContentPage
{
    private PersonDetailViewModel _PersonDetailViewModel;


    public PersonDetails(PersonDetailViewModel viewModel)
	{
		InitializeComponent();
        _PersonDetailViewModel = viewModel;
        BindingContext = _PersonDetailViewModel;
        _PersonDetailViewModel.PropertyChanged += OnViewModelPropertyChanged;
        BindHistoryVisitsGrid();
    }

    private void BindHistoryVisitsGrid()
    {
        if (_PersonDetailViewModel.PatientApnts != null)
        {
            //Medical History 
            HistoryGrid.RowDefinitions.Clear();

            if (_PersonDetailViewModel.PatientApnts != null)
            {
                foreach (var appointment in _PersonDetailViewModel.PatientApnts)
                {
                    if (appointment != null)
                    {
                        if (appointment.Notes != null)
                        {
                            foreach (var item in appointment.Notes)
                            {
                                int i = HistoryGrid.RowDefinitions.Count;
                                HistoryGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                                var notelbl = new Label { Text = item, FontAttributes = FontAttributes.Bold, FontSize = 14, TextColor = Colors.Black };
                                Grid.SetRow(notelbl, i);

                                HistoryGrid.Children.Add(notelbl);
                            }
                        }
                        if (appointment.Reminders != null)
                        {
                            foreach (var item in appointment.Reminders)
                            {
                                int i = HistoryGrid.RowDefinitions.Count;
                                HistoryGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                                var remlbl = new Label { Text = item, FontAttributes = FontAttributes.Bold, FontSize = 14, TextColor = Colors.Black };
                                Grid.SetRow(remlbl, i);

                                HistoryGrid.Children.Add(remlbl);
                            }
                        }
                    }
                }
            }


            //Recent Visits

            RecentVisitsGrid.RowDefinitions.Clear();
            if (_PersonDetailViewModel.PatientApnts != null)
            {
                foreach (var appointment in _PersonDetailViewModel.PatientApnts)
                {
                    if (appointment != null)
                    {
                        int i = RecentVisitsGrid.RowDefinitions.Count;
                        RecentVisitsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        var remlbl = new Label { Text = appointment.AptnDate.ToString("MMM dd,yyyy HH:ss tt"), FontAttributes = FontAttributes.Bold, FontSize = 14, TextColor = Colors.Black };
                        Grid.SetRow(remlbl, i);

                        RecentVisitsGrid.Children.Add(remlbl);
                    }
                }
            }
        }
    }

    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PersonDetailViewModel.PatientName))
        {
            BindHistoryVisitsGrid();
        }
    }

}
