using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDMA.Core.Logging
{
    public static class TypeExtensions
    {
        public static string GetTypeOutput(this Type t)
        {
            using(CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider())
            {
                CodeTypeReference type = new CodeTypeReference(t);
                return cSharpCodeProvider.GetTypeOutput(type);
            }
        }
    }
}
