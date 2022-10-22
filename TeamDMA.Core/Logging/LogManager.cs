using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDMA.Core.Logging
{
    public static class LogManager
    {
        public static ILogger GetLogger<T>()
        {
            return new Logger(typeof(T));
        }
    }
}
