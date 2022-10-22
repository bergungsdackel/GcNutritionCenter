using GcNutritionCenter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TeamDMA.Core.Helper;
using TeamDMA.Core.Logging;

namespace GcNutritionCenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILogger Logger = LogManager.GetLogger<App>();

        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Info("=== STARTING LOGGING");

            string procName = Process.GetCurrentProcess().ProcessName;     
            Process[] processes = Process.GetProcessesByName(procName);

            if (processes.Length > 1) // running
            {
                // TODO: Nice message box to show the user that it is already running
                MessageBox.Show(procName + " läuft bereits");
                return;
            }
            else
            {
                MainWindow = new MainWindow()
                {
                    DataContext = new MainWindowViewModel(this)
                };

                MainWindow.Show();

                base.OnStartup(e);
            }
        }
    }
}
