using System;
using System.IO;

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
            catch (IndexOutOfRangeException)
            {
                path = System.Reflection.Assembly.GetExecutingAssembly().Location; // Use location of exe if no path provided
                path = Path.GetDirectoryName(path);
            }

            if(Directory.Exists(path)) 
            {
                // This path is a directory
                Console.WriteLine("Splitting steam screenshots into subfolders...");
                var sorter = new SteamScreenshotSorter();
                sorter.Sort(path);
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
    }
}

// TODO:
// Implement UI
// UI - let the user navigate to their screenshot directory
// UI - show a progress bar