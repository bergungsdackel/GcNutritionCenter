using GcNutritionCenter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GcNutritionCenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
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
