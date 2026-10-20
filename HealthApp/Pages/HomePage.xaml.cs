using HealthApp.Model;
using HealthApp.ViewModel;

namespace HealthApp;

public partial class HomePage : ContentPage
{
    public HomePage(HomePageViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
        //_ = viewModel.LoadAsync(GlobalVars.userId);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await ((HomePageViewModel)BindingContext)
                .LoadAsync(GlobalVars.userId);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Home page error", ex.Message, "OK");
        }
    }
}
