using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Structure_duplicator
{
    class Program
    {
        static string fromPath;
        static string toPath;
        static void Main(string[] args)
        {
            Console.WriteLine("Where are the real files?");
            fromPath = Console.ReadLine();
            Console.WriteLine("Where are the virtual files?");
            toPath = Console.ReadLine();


            IEnumerable<string> searchResults = Directory.EnumerateDirectories(fromPath, "*", SearchOption.AllDirectories);
            foreach(string dir in searchResults)
            {
                string newDir = RebasePath(dir);
                if (!Directory.Exists(newDir))
                {
                    Directory.CreateDirectory(newDir);
                }
            }
            searchResults = Directory.EnumerateFiles(fromPath, "*", SearchOption.AllDirectories);
            foreach(string file in searchResults)
            {
                string newFile = RebasePath(file);
                if (!File.Exists(newFile))
                {
                    File.Create(newFile);
                }
            }

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
        static void ProcessDirectory(string directoryPath)
        {

        }
        public static string RebasePath(string fullPath)
        {
            return Path.Combine(toPath, GetRelativePath(fullPath, fromPath));
        }
        public static string GetRelativePath(string fullPath, string basePath)
        {
            // Require trailing backslash for path
            if (!basePath.EndsWith("\\"))
                basePath += "\\";

            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slashes
            return relativeUri.ToString().Replace("/", "\\").Replace("%20"," ");

        }
    }
}
