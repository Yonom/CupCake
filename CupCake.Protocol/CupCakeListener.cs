using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ProtoBuf;

namespace CupCake.Protocol
{
    public class CupCakeListener
    {
        public EndPoint EndPoint { get; private set; }

        public CupCakeListener(IPAddress ipAddress, int port, Action<TcpClient, NetworkStream> callback)
        {
            var server = new TcpListener(ipAddress, port);
            server.Start();

            this.EndPoint = server.Server.LocalEndPoint;

            new Thread(() =>
            {
                while (true)
                {
                    var client = server.AcceptTcpClient();
                    this.HandleConnectionNewThread(client, callback);
                }
            }) { IsBackground = true }.Start();
        }

        private void HandleConnectionNewThread(TcpClient client, Action<TcpClient, NetworkStream> callback)
        {
            new Thread(() => this.HandleConnection(client, callback)) {IsBackground = true}.Start();
        }

        private void HandleConnection(TcpClient state, Action<TcpClient, NetworkStream> callback)
        {
            try
            {
                using (var client = state)
                using (NetworkStream stream = client.GetStream())
                {
                    Serializer.SerializeWithLengthPrefix(stream, new Hello {Version = Hello.VersionNumber},
                        PrefixStyle.Base128);
                    var hello = this.Get<Hello>(stream);
                    if (hello.Version != Hello.VersionNumber)
                    {
                        throw new InvalidDataException("Server and Client version numbers do not match.");
                    }

                    callback(client, stream);
                }
            }
            catch (SocketException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            catch (InvalidDataException ex)
            {
                Debug.WriteLine("Failed to connect: " + ex.Message);
            }
            catch (IOException ex)
            {
                Debug.WriteLine("Error while reading: " + ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                Debug.WriteLine("Connection was closed. Got ObjectDisposedException: " + ex.Message);
            }
        }

        public void Connect(IPEndPoint endPoint, Action<TcpClient, NetworkStream> callback)
        {
            var client = new TcpClient();
            client.Connect(endPoint);
            this.HandleConnectionNewThread(client, callback);
        }

        public T Get<T>(NetworkStream stream)
        {
            return Serializer.DeserializeWithLengthPrefix<T>(stream, PrefixStyle.Base128);
        }

        public void Send(NetworkStream stream, Message message)
        {
            stream.WriteByte((byte)message);
        }

        public void Send<T>(NetworkStream stream, Message message, T messageObj)
        {
            stream.WriteByte((byte)message);
            Serializer.SerializeWithLengthPrefix(stream, messageObj, PrefixStyle.Base128);
        }
    }
}
