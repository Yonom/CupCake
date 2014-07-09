using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CupCake.Debug
{
    public static class CupCakeLoader
    {
        public static void LoadCupCake(IEnumerable<string> args)
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

            AppDomain.CurrentDomain.ExecuteAssembly(path + "\\CupCake.Server.exe",
                new[] { "--envpath", path }.Concat(args).ToArray());
        }
    }
}
