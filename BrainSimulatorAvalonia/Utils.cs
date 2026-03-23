using System;
using System.IO;

namespace BrainSimulatorAvalonia
{
    public static class Utils
    {
        public static string RebaseFolderToCurrentDevEnvironment(string fullPath)
        {
            int index = fullPath.ToLower().IndexOf("\\networks\\");
            if (index != -1)
            {
                fullPath = fullPath.Substring(index);
                string Path1 = Path.GetFullPath(".");
                string Path2 = Path1.Replace("\\bin\\Debug\\net6.0-windows", "");
                fullPath = Path2 + fullPath;
            }
            return fullPath;
        }
    }
}
