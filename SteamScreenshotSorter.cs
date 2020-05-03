using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace SteamScreenshotSorter
{
    public class SteamScreenshotSorter
    {
        private Dictionary<int, string> _appIdDict = new Dictionary<int, string>();

        public SteamScreenshotSorter()
        {
            _appIdDict.Add(0, "Unsorted"); // This prevents an error when accessing the API with appid 0
        }

        public void Sort(string path)
        {
            string [] fileEntries = Directory.GetFiles(path);
            fileEntries = fileEntries.Where(f => Path.GetExtension(f) == ".png").ToArray(); // Filter out all non-png files (e.g. executable)
            
            foreach(string fileName in fileEntries)
            {
                ProcessFile(fileName);
            }
        }

        public void ProcessFile(string path) 
        {
            var newFileName = FileAndDirectoryHelper.GetTrimmedFileNameFromPath(path);
            var newLocation = GenerateNewLocation(path, newFileName);

            new System.IO.FileInfo(newLocation).Directory.Create(); // Create the new folder if it doesn't exist
            File.Move(path, newLocation);

            Console.WriteLine("Processed file '{0}'.", path);
        }

        private string GenerateNewLocation(string oldPath, string newFileName)
        {
            var appId = FileAndDirectoryHelper.GetFileNamePrefixFromPath(oldPath, '_');
            var newFolder = GetGameNameFromAppId(int.Parse(appId));

            var newPath = oldPath.Substring(0, oldPath.LastIndexOf('\\'));
            newPath += "\\" + newFolder;
            newPath += "\\" + newFileName;

            return newPath;
        }

        private string GetGameNameFromAppId(int appId)
        {
            if (_appIdDict.ContainsKey(appId))
            {
                return _appIdDict.GetValueOrDefault(appId);
            }
            else
            {
                Console.WriteLine("Fetching name for appid " + appId + " from steam api...");
                var name = GetAppNameFromApi(appId);
                Console.WriteLine("Game is " + name);

                // Store this ID-name pair locally so we don't need to hit the API again
                _appIdDict.Add(appId, name);

                return name;
            }
        }

        private static string GetAppNameFromApi(int appId)
        {
            string html = string.Empty;
            string url = @"https://store.steampowered.com/api/appdetails?appids=" + appId;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Regex nameRegex = new Regex(@"(?<=""name"":"")[^""]*");

            var name = nameRegex.Matches(html).First().Groups[0].Value;
            return RemoveUnicodeFromString(name);
        }

        private static string RemoveUnicodeFromString(string input)
        {
            var result = input;
            while (result.Contains(@"\u"))
            {
                // Unicode characters are in form '\u9999'
                result = result.Remove(result.IndexOf(@"\u"), 6); 
            }
            return result;            
        }

    }
}