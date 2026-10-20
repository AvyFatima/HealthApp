using System;
using System.Collections.Generic;

namespace HealthApp.Model
{
    public static class GlobalVars
    {

        private const int EmpId = 0;

        // This property wraps the native storage read/write mechanism
        public static int userId
        {
            get => Preferences.Default.Get(nameof(userId), EmpId); // Returns EmpId if the key doesn't exist yet
            set => Preferences.Default.Set(nameof(userId), value); // Directly writes the new value to the device
        }
    }

    public static class AppConstants
    {
        // Using a read-only array to prevent other classes from accidentally modifying it
        public static readonly string[] AllowedDateFormats = new string[]
        {
            "dd/MM/yyyy HH:mm:ss",    // Matches: "18/09/2026 11:30:00"
            "dd-MM-yyyy hh:mm tt",    // Matches: "18-09-2026 10:00 AM"
            "d-M-yyyy h:mm tt",       // Matches single-digit variations with hyphens
            "d/M/yyyy HH:mm:ss"       // Matches single-digit variations with slashes
        };

        public static readonly string StandardDateFormat = "dd/MM/yyyy HH:mm:ss";

    }

    public static class AuthService
    {
        private const string LoggedInKey = "user_is_logged_in";

        // This property wraps the native storage read/write mechanism
        public static int IsLoggedIn
        {
            get => Preferences.Default.Get(LoggedInKey, 0); // Returns 0 if the key doesn't exist yet
            set => Preferences.Default.Set(LoggedInKey, value); // Directly writes the new value to the device
        }
    }
}

