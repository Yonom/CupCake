using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CupCake.Debug
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                string command = args[0].ToLower();
                IEnumerable<string> newArgs = args.Skip(1);

                if (command == "deploy")
                {
                    Deploy(newArgs);
                }
                else if (command == "debug")
                {
                    Debug(newArgs);
                }
                else
                {
                    Console.WriteLine("Invalid command specified.");
                    return 1;
                }
            }
            else
            {
                Console.WriteLine("Please specify a command to run");
                return 1;
            }
            return 0;
        }

        private static void Deploy(IEnumerable<string> args)
        {
            string cupCakePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CupCake";
            if (!Directory.Exists(cupCakePath))
                Directory.CreateDirectory(cupCakePath);

            string profilesPath = cupCakePath + "\\Profiles";
            if (!Directory.Exists(profilesPath))
                Directory.CreateDirectory(profilesPath);

            string debugProfilePath = profilesPath + "\\Debug";
            if (Directory.Exists(debugProfilePath))
                DeleteFiles(debugProfilePath);

            Directory.CreateDirectory(debugProfilePath);

            string sourcePath = String.Join(" ", args);

            string[] files = Directory.GetFiles(sourcePath);

            foreach (string s in files)
            {
                string fileName = Path.GetFileName(s);
                if (fileName != null)
                {
                    string destFile = Path.Combine(debugProfilePath, fileName);
                    File.Copy(s, destFile, true);
                }
            }
        }

        public static void DeleteFiles(string path)
        {
            foreach (var file in new DirectoryInfo(path).GetFiles())
            {
                file.Delete();
            }
        }

        private static void Debug(IEnumerable<string> args)
        {
            CupCakeLoader.LoadCupCake(new[] {"--debug"}.Concat(args));
        }
    }
}