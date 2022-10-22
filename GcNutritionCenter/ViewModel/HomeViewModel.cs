using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamDMA.Core.ViewModel;

namespace GcNutritionCenter
{
    internal class HomeViewModel : BaseViewModel
    {
        public string Version
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version ?? new Version();
                if (version.Major < 1) 
                    return $"BETA {version.ToString()}";
                else
                    return version.ToString();
            }
        }

        public HomeViewModel(object parent) : base(parent)
        {

        }
    }
}
