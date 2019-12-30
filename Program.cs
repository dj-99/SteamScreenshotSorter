using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace SteamScreenshotSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = String.Empty;

            try
            {
                path = args[0];
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new ArgumentOutOfRangeException("Please provide a path to a directory", e);
            }

            if(Directory.Exists(path)) 
            {
                // This path is a directory
                Console.WriteLine("Splitting steam screenshots into subfolders...");
                ProcessDirectory(path);
            }               
            else if(File.Exists(path)) 
            {
                // This path is a file
                Console.WriteLine("{0} is a file, please provide a directory.", path);
            }
            else 
            {
                Console.WriteLine("{0} is not a valid directory.", path);
            } 
        }

        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory) 
        {
            // Process the list of files found in the directory.
            string [] fileEntries = Directory.GetFiles(targetDirectory);
            foreach(string fileName in fileEntries)
                ProcessFile(fileName);
        }
            
        // Insert logic for processing found files here.
        public static void ProcessFile(string path) 
        {
            var newFileName = GetTrimmedFileName(path);
            var newLocation = GenerateNewLocation(path, newFileName);
            new System.IO.FileInfo(newLocation).Directory.Create();
            File.Move(path, newLocation);

            Console.WriteLine("Processed file '{0}'.", path);	    
        }

        private static string GetTrimmedFileName(string path)
        {
            var fileName = GetFileNameFromPath(path);
            var prefixToRemove = fileName.Split('_').First();
            return fileName.Replace(prefixToRemove + "_", null);
        }

        private static string GetFileNameFromPath(string path)
        {
            return path.Split('\\').Last();
        }

        private static string GenerateNewLocation(string oldPath, string newFileName)
        {
            var appId = GetFileNameFromPath(oldPath).Split('_').First();
            var newFolder = GetGameNameFromAppId(appId);

            var newPath = oldPath.Substring(0, oldPath.LastIndexOf('\\'));
            newPath += "\\" + newFolder;
            newPath += "\\" + newFileName;

            return newPath;
        }

        private static object GetGameNameFromAppId(string appId)
        {
            string html = string.Empty;
            string url = @"https://store.steampowered.com/api/appdetails?appids=" + appId;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            Console.WriteLine("Getting name for appid " + appId);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Regex nameRegex = new Regex(@"(?<=""name"":"")[^""]*");

            var name = nameRegex.Matches(html).First().Groups[0].Value;
            Console.WriteLine("Game is " + name);

            return name;
        }
    }
}

// TODO:
// Store appids in a global dictionary, check to see if appid is in dictionary before making request
// UI - let the user navigate to their screenshot directory
// UI - show a progress bar