using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PasswordManagerSSDw41
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set culture to invariant for the entire application
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            // Example of using UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Initialize application configuration
            ApplicationConfiguration.Initialize();

            // Check if the initial setup is completed
            if (File.Exists("SetupCompleted.txt"))
            {
                Application.Run(new LoginForm());
            }
            else
            {
                Application.Run(new InitialSetupForm());
            }
        }
    }
}