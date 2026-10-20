# HealthApp

HealthApp is a cross-platform healthcare management application built with .NET MAUI. It provides employees and healthcare staff with tools for authentication, patient records, appointment management, and dashboard-based access to daily healthcare information.

## Features

- Employee authentication and session management
- Employee dashboard with:
  - Today's appointments
  - Next scheduled appointment
  - Employee information
- Patient management:
  - View patient records
  - Add patients
  - Update patient information
  - Delete patient records
- Appointment management:
  - View all appointments
  - View appointments by patient
  - View today's appointments
  - View the next upcoming appointment
  - Add, update, and delete appointments
  - View appointment details
- Employee and patient data access through repository services
- XML-based local data persistence
- Cross-platform user interface
- Custom application branding, icons, fonts, and visual resources

## Technology Stack

- **Language:** C#
- **UI Framework:** .NET MAUI
- **UI Markup:** XAML
- **Target Framework:** .NET 7
- **Platforms:**
  - Android
  - iOS
  - macOS Catalyst
  - Windows
  - Tizen support is included in the project structure but is currently disabled in the project configuration
- **Application Architecture:**
  - MVVM-inspired structure
  - Services and repository pattern
  - Dependency injection
  - Interface-based abstractions
- **MVVM Toolkit:** CommunityToolkit.Mvvm
- **Data Storage:** XML files
- **Logging:** Microsoft.Extensions.Logging.Debug
- **Fonts:** Open Sans

## Project Structure

```text
HealthApp/
├── HealthApp/
│   ├── Adapter/          # XML storage and serialization adapters
│   ├── DataMap/          # Data mapping utilities
│   ├── Interfaces/       # Repository and service contracts
│   ├── Model/            # Application data models
│   ├── Pages/            # XAML application pages
│   ├── Persistence/      # Repository implementations
│   ├── Platforms/        # Platform-specific configuration
│   ├── Resources/        # Images, fonts, styles, icons, and XML data
│   ├── Services/         # Authentication, appointment, patient, and navigation services
│   ├── ViewModel/        # View models for application pages
│   ├── App.xaml          # Application resources and startup configuration
│   ├── AppShell.xaml     # Shell navigation configuration
│   ├── MauiProgram.cs    # Dependency injection and application setup
│   └── HealthApp.csproj  # Project configuration
└── HealthApp.sln         # Visual Studio solution
```

## Requirements

To build and run the project, install:

- Visual Studio 2022 with the .NET MAUI workload, or
- The .NET 7 SDK with the required .NET MAUI workloads
- Platform-specific development tools for the target platform:
  - Android SDK and emulator for Android
  - Xcode for iOS and macOS Catalyst
  - Windows App SDK tools for Windows

> Note: .NET 7 is no longer supported by Microsoft. For production use, consider upgrading the project to a supported .NET version.

## Getting Started

### Clone the repository

```bash
git clone https://github.com/AvyFatima/HealthApp.git
cd HealthApp
```

### Open the project

Open `HealthApp.sln` in Visual Studio.

### Restore dependencies

```bash
dotnet restore
```

### Build the project

```bash
dotnet build
```

### Run the application

The application can be launched from Visual Studio by selecting an available target device or emulator.

Depending on the platform, the project targets frameworks such as:

```text
net7.0-android
net7.0-ios
net7.0-maccatalyst
net7.0-windows10.0.19041.0
```

## Data Storage

The application currently uses XML files for local data storage. The bundled data files are located in:

```text
HealthApp/Resources/Raw/Database/
```

These files include:

- `AppointmentsDB.xml`
- `EmpDB.xml`
- `PatientsDB.xml`

The repository and adapter layers provide access to employee, patient, and appointment data.

## Architecture

HealthApp separates application responsibilities into several layers:

- **Pages:** Define the user interface using XAML and code-behind files.
- **ViewModels:** Connect the user interface to application logic.
- **Services:** Provide authentication, navigation, dashboard, patient, and appointment functionality.
- **Repositories:** Manage access to employee, patient, and appointment data.
- **Adapters:** Handle XML serialization and storage operations.
- **Models:** Represent employees, patients, appointments, and dashboard data.
- **Dependency Injection:** Services, repositories, pages, and view models are registered in `MauiProgram.cs`.

## Contributing

Contributions are welcome. To contribute:

1. Fork the repository.
2. Create a feature branch:

   ```bash
   git checkout -b feature/your-feature
   ```

3. Make your changes.
4. Build the project and verify that it works.
5. Commit your changes:

   ```bash
   git commit -m "Add your feature"
   ```

6. Push your branch:

   ```bash
   git push origin feature/your-feature
   ```

7. Open a pull request.

## License

No license has currently been specified for this project. Add a license before distributing or reusing the code.

## Disclaimer

HealthApp is intended for educational and application-development purposes. It is not a substitute for professional medical advice, diagnosis, or treatment.
