using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamDMA.Core.Logging;

namespace TeamDMA.Core.Helper
{
    public static class Configuration
    {
        private const string alternativeAssemblyName = "TeamDmaCore";

        public static string GetCurrentAppDataDir(System.Reflection.Assembly? callingAssembly = null)
        {
            try
            {
                if (callingAssembly == null)
                {
                    //callingAssembly = System.Reflection.Assembly.GetCallingAssembly();
                    callingAssembly = System.Reflection.Assembly.GetEntryAssembly();
                }
                string appdata = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), callingAssembly!.GetName().Name ?? alternativeAssemblyName);
                if (!Directory.Exists(appdata)) Directory.CreateDirectory(appdata);
                return appdata;
            }
            catch(Exception)
            {
                // TODO: Logger?
                return String.Empty;
            }
        }

        public static string GetCallingAssemblyName()
        {
            return System.Reflection.Assembly.GetCallingAssembly().GetName().Name ?? alternativeAssemblyName;
        }

        public static string GetEntryAssemblyName()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Name ?? alternativeAssemblyName;
        }
    }
}
