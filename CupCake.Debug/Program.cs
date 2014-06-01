using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CupCake.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            var cupCakePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CupCake";
            if (!Directory.Exists(cupCakePath))
                Directory.CreateDirectory(cupCakePath);

           var  profilesPath = cupCakePath + "\\Profiles";
            if (!Directory.Exists(profilesPath))
                Directory.CreateDirectory(profilesPath);

            var debugProfilePath = profilesPath + "\\Debug";
            if (Directory.Exists(debugProfilePath))
                Directory.Delete(debugProfilePath, true);

            Directory.CreateDirectory(debugProfilePath);

            var sourcePath = String.Join(" ", args);
            if (Directory.Exists(sourcePath))
            {
                string[] files = Directory.GetFiles(sourcePath);

                foreach (string s in files)
                {
                    var fileName = Path.GetFileName(s);
                    var destFile = Path.Combine(debugProfilePath, fileName);
                    File.Copy(s, destFile, true);
                }
            }
        }
    }
}
