using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace r6random
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Handle UI thread exceptions
            Application.ThreadException += Application_ThreadException;

            // Handle non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new Form1());
        }
        // UI thread exceptions
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ShowErrorAndExit(e.Exception);
        }

        // Non-UI thread exceptions
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                ShowErrorAndExit(ex);
            }
        }

        // Common method to show error message and exit
        private static void ShowErrorAndExit(Exception ex)
        {
            MessageBox.Show(
                "An unexpected error occurred:\n\n" + ex.Message +
                "\n\nThe application will now close.",
                "Application Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            Environment.Exit(1); // close the app
        }
    }
}
