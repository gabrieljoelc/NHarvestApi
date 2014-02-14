using System;
using System.Diagnostics;

namespace NHarvestApi
{
    [DebuggerNonUserCode]
    static class Guard
    {
        public static void IsNotNull(object obj, string parameter)
        {
            if (obj == null)
                throw new ArgumentNullException(parameter);
        }
        public static void IsNotNullOrEmpty(string str, string parameter)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(parameter);
        }
    }
}