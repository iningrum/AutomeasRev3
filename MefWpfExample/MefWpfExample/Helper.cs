using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimLabs.Utility;

namespace MefWpfExample
{
    public static class Helper
    {
        /// <summary>
        /// Gets the path of a specified directory. Starting point is the given directory path
        /// </summary>
        /// <param name="name">The name of the directory to be searched for</param>
        /// <param name="directory">The path of the start directory (optional, when empty the current directory will be chosen)</param>
        /// <returns>The full path of the directory to be searched for</returns>
        public static string GetDirectory(string name, string directory = "")
        {
            if (string.IsNullOrEmpty(directory))
                directory = Global.GetBaseFolder();

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var dirInfo = new DirectoryInfo(directory);
            if (!dirInfo.Exists)
            {
                return "";
            }

            var subDirs = dirInfo.GetDirectories("*", SearchOption.AllDirectories);

            var path = subDirs.FirstOrDefault(f => f.FullName.Contains(name));
            if (path == null)
                return dirInfo.Parent != null ? GetDirectory(name, dirInfo.Parent.FullName) : "";

            return path.FullName;
        }
    }
}
