using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CupCake.Debug
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                var command = args[0].ToLower();
                var newArgs = args.Skip(1);

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
                DeleteDirectory(debugProfilePath);

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

        /// <summary>
        /// Depth-first recursive delete, with handling for descendant 
        /// directories open in Windows Explorer.
        /// </summary>
        public static void DeleteDirectory(string path)
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, true);
            }
        }

        private static void Debug(IEnumerable<string> args)
        {
            string path;
            try
            {
                using (var client = new TcpClient())
                {
                    client.Connect(IPAddress.Loopback, 4570);

                    NetworkStream stream = client.GetStream();
                    using (var reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        // Request Debug
                        stream.WriteByte(0xE1);

                        // Skip one byte
                        stream.ReadByte();
                        // Wait until the connection is closed by cupcake
                        path = reader.ReadToEnd();
                    }
                }
            }
            catch (SocketException ex)
            {
                throw new ConnectionException("Problem communicating with the client, make sure it is running.", ex);
            }

            AppDomain.CurrentDomain.ExecuteAssembly(path + "\\CupCake.Server.exe", new[] { "--envpath", path, "--debug"}.Concat(args).ToArray());
        }
    }
}