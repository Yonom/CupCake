using System.Runtime.InteropServices;

namespace CupCake.Client
{
    public static class FileHelper
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFile(string name);

        public static void UnblockPath(string path)
        {
            string[] files = System.IO.Directory.GetFiles(path);
            string[] dirs = System.IO.Directory.GetDirectories(path);

            foreach (string file in files)
            {
                UnblockFile(file);
            }

            foreach (string dir in dirs)
            {
                UnblockPath(dir);
            }

        }

        public static bool UnblockFile(string fileName)
        {
            return DeleteFile(fileName + ":Zone.Identifier");
        }
    }
}
