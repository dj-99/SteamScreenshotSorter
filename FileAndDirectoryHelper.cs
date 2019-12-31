using System.IO;
using System.Linq;

namespace SteamScreenshotSorter
{
    public class FileAndDirectoryHelper
    {
        public static string GetFileNamePrefixFromPath(string path, char separator)
        {
            return GetPrefixFromFileName(Path.GetFileName(path), separator);
        }

        public static string GetPrefixFromFileName(string name, char separator)
        {
            return name.Split(separator).First();
        }

        public static string GetTrimmedFileNameFromPath(string path)
        {
            var fileName = Path.GetFileName(path);
            var prefix = GetPrefixFromFileName(fileName, '_');
            return fileName.Replace(prefix + "_", null);
        }
    }
}