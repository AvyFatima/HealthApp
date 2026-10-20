using HealthApp.Interfaces;
using HealthApp.Persistence;
using HealthApp.Services;
using HealthApp.ViewModel;
using Microsoft.Extensions.Logging;

namespace HealthApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
		builder.Services.AddSingleton<IPatientRepository, PatientRepository>();
		builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
		
		builder.Services.AddSingleton<IAppointmentService, AppointmentService>();
		builder.Services.AddSingleton<IHomeDashboardService, HomeDashboardService>();
		builder.Services.AddSingleton<IPatientService, PatientService>();
		builder.Services.AddSingleton<IUserSession, UserSession>();


		builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
		builder.Services.AddSingleton<INavigationService, ShellNavigator>();
		builder.Services.AddSingleton<IUserMessageService, MauiUserMessageService>();

        builder.Services.AddTransient<LoginPageViewModel>();
        builder.Services.AddTransient<HomePageViewModel>();
		builder.Services.AddTransient<PersonDetailViewModel>();
		builder.Services.AddTransient<AppointmentDetailViewModel>();
		builder.Services.AddTransient<PatientListViewModel>();
		builder.Services.AddTransient<AppointmentListViewModel>();

		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<ListRecords>();
		builder.Services.AddTransient<PersonDetails>();
		builder.Services.AddTransient<AppointmentDetail>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
