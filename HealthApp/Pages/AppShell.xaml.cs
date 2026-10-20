namespace HealthApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        // Register the page route name

        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        Routing.RegisterRoute("HomePage", typeof(HomePage));
        Routing.RegisterRoute("AppointmentDetail", typeof(AppointmentDetail));
        Routing.RegisterRoute("ListRecords", typeof(ListRecords));
        Routing.RegisterRoute("PersonDetails", typeof(PersonDetails));

    }
}

