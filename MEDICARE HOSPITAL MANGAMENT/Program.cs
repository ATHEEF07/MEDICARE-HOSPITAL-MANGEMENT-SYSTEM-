using System;
using System.Windows.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Forms;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;

namespace MEDICARE_HOSPITAL_MANGAMENT
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Configures global exception handling, database probes, authentication, and the Master Shell.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            // Test runner hooks for automated verification
            if (args.Length > 0 && args[0] == "--test-phase5")
            {
                return Phase5TestRunner.RunAllTests();
            }

            if (args.Length > 0 && args[0] == "--test-phase6")
            {
                return Phase6TestRunner.RunAllTests();
            }

            ApplicationConfiguration.Initialize();

            // Configure global defensive exception handlers
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show(
                    $"An unhandled application error occurred:\n{e.Exception.Message}",
                    "MediCare System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show(
                        $"A critical system exception was encountered:\n{ex.Message}",
                        "MediCare Fatal Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
            };

            // Non-blocking database connectivity check
            bool isDbOnline = DatabaseHelper.TestConnection();
            if (!isDbOnline)
            {
                // Inform user without crashing - allows testing UI and fallback states
                // Suppressed for fast boot, status is displayed in MainForm sidebar
            }

            // Launch modal authentication dialog
            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK && SessionManager.IsLoggedIn)
                {
                    Application.Run(new MainForm());
                }
            }

            return 0;
        }
    }
}