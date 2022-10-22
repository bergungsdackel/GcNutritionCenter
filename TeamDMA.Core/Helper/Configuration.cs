using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDMA.Core.Helper
{
    public static class Configuration
    {
        private const string alternativeAssemblyName = "TeamDmaCore";

        public static string GetCurrentAppDataDir(System.Reflection.Assembly? callingAssembly = null)
        {
            if(callingAssembly == null)
            {
                callingAssembly = System.Reflection.Assembly.GetCallingAssembly();
            }
            string appdata = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), callingAssembly.GetName().Name ?? alternativeAssemblyName);
            if(!Directory.Exists(appdata)) Directory.CreateDirectory(appdata);
            return appdata;
        }

        public static string GetCurrentAssemblyName()
        {
            return System.Reflection.Assembly.GetCallingAssembly().GetName().Name ?? alternativeAssemblyName;
        }
    }
}
