using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamDMA.Core.ViewModel;
using TeamDMA.Core.Logging;
using System.Collections.ObjectModel;
using TeamDMA.Core.Helper;

namespace GcNutritionCenter
{
    internal class SettingsViewModel : BaseViewModel
    {

        private const string fileName = "settings.json";

        private static readonly ILogger Logger = LogManager.GetLogger<SettingsViewModel>();

        private Model.Configuration? _settings;
        public Model.Configuration? Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                SetProperty(ref _settings, value);
            }
        }

        public bool DeleteCustomerIfZero
        {
            get
            {
                if(this.Settings != null)
                {
                    return this.Settings.DeleteCustomerIfZero;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Settings != null) this.Settings.DeleteCustomerIfZero = value;
                this.Save(); // ugly
            }
        }

        public SettingsViewModel(object parent) : base(parent)
        {
            this.Settings = new Model.Configuration();
            Model.Configuration? _settings = JsonFile.ReadFromFile<Model.Configuration>(fileName);
            if (_settings != null)
            {
                this.Settings = _settings;
            }            
        }

        public void Save()
        {
            // convert in json and save
            JsonFile.SaveToFile(Settings, fileName);
        }
    }
}
