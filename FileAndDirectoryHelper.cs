using System.Linq;

namespace SteamScreenshotSorter
{
    public class FileAndDirectoryHelper
    {
        public static string GetFileNameFromPath(string path)
        {
            return path.Split('\\').Last();
        }

        public static string GetFileNamePrefixFromPath(string path, char separator)
        {
            return GetPrefixFromFileName(GetFileNameFromPath(path), separator);
        }

        public static string GetPrefixFromFileName(string name, char separator)
        {
            return name.Split(separator).First();
        }

        public static string GetTrimmedFileName(string path)
        {
            var fileName = GetFileNameFromPath(path);
            var prefix = GetPrefixFromFileName(fileName, '_');
            return fileName.Replace(prefix + "_", null);
        }
    }
}