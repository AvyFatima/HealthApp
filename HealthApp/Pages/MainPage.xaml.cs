
using HealthApp.Model;

namespace HealthApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, EventArgs e)
    {
        try
        {
            await LoadDbAsync();

            var route = GlobalVars.userId != 0
                ? "HomePage"
                : "LoginPage";

            System.Diagnostics.Debug.WriteLine(
                $"Initial route: {route}, user ID: {GlobalVars.userId}");

            await Shell.Current.GoToAsync(route);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Initial navigation failed: {ex}");

            await DisplayAlert(
                "Startup error",
                ex.Message,
                "OK");
        }
    }

    private async Task LoadDbAsync()
    {
        await Task.WhenAll(
            InitializeAndCopyDatabaseAsync("EmpDB"),
            InitializeAndCopyDatabaseAsync("AppointmentsDB"),
            InitializeAndCopyDatabaseAsync("PatientsDB"));
    }

    private static async Task InitializeAndCopyDatabaseAsync(string database)
    {
        // 1. Where the emp file will permanently live on the device
        string destinationPath = Path.Combine(FileSystem.AppDataDirectory, "Database", database + ".xml");

        // 2. Only copy it if it doesn't already exist in the safe folder
        if (!File.Exists(destinationPath))
        {
            // Create the directory structure on the device filesystem
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

            // 3. Open the file we bundled inside Resources/Raw/Database/
            using Stream assetStream = await FileSystem.OpenAppPackageFileAsync("Database/" + database + ".xml");

            // 4. Create a fresh target stream at the secure writeable location
            using FileStream fileStream = File.Create(destinationPath);

            // 5. This performs the physical transfer on the machine/device
            await assetStream.CopyToAsync(fileStream);
        }
    }
   
}

