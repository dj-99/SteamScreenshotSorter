using System;
using System.IO;
using System.Linq;

namespace SteamScreenshotSorter
{
    public static class FileAndDirectoryHelper
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

        public static string RemoveUnicode(this string input)
        {
            var result = input;
            while (result.Contains(@"\u"))
            {
                // Unicode characters are in form '\u9999'
                result = result.Remove(result.IndexOf(@"\u"), 6);
            }
            return result;
        }

        public static string RemoveInvalidPathChars(this string input)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                input = input.Replace(c.ToString(), "");
            }
            return input;
        }
    }
}