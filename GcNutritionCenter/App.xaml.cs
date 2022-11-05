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
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GcNutritionCenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILogger Logger = LogManager.GetLogger<App>();

        //public IConfigurationRoot Configuration { get; private set; }

        //private const string settingsFileName = "settings.json";

        protected override void OnStartup(StartupEventArgs e)
        {
            try
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
                    //// config
                    //string pathToSettingsDir = TeamDMA.Core.Helper.Configuration.GetCurrentAppDataDir();
                    //Directory.CreateDirectory(pathToSettingsDir);
                    //var completePath = Path.Combine(pathToSettingsDir, settingsFileName);
                    //if(!File.Exists(completePath))
                    //{
                    //    File.Create(completePath);
                    //}
                    //var builder = new ConfigurationBuilder()
                    //    .SetBasePath(pathToSettingsDir)
                    //    .AddJsonFile(settingsFileName, optional: false, reloadOnChange: true);

                    //Configuration = builder.Build();
                    ////

                    MainWindow = new MainWindow()
                    {
                        DataContext = new MainWindowViewModel(this)
                    };

                    MainWindow.Show();

                    base.OnStartup(e);
                }
            }
            catch(Exception ex)
            {
                Logger.Fatal("Unhandled exception in App", ex);
                throw;
            }
        }
    }
}
