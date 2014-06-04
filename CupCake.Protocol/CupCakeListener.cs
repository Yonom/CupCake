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
        public CupCakeListener(IPAddress ipAddress, int port, Action<TcpClient, NetworkStream> callback)
        {
            var server = new TcpListener(ipAddress, port);
            server.Start();

            this.EndPoint = server.Server.LocalEndPoint;

            new Thread(() =>
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    this.HandleConnectionNewThread(client, callback);
                }
            }) {IsBackground = true}.Start();
        }

        public EndPoint EndPoint { get; private set; }

        public event Action<Stream> DebugRequest;

        protected virtual void OnDebugRequest(Stream stream)
        {
            Action<Stream> handler = this.DebugRequest;
            if (handler != null) handler(stream);
        }

        private void HandleConnectionNewThread(TcpClient client, Action<TcpClient, NetworkStream> callback)
        {
            new Thread(() => this.HandleConnection(client, callback)) {IsBackground = true}.Start();
        }

        private void HandleConnection(TcpClient state, Action<TcpClient, NetworkStream> callback)
        {
            try
            {
                using (TcpClient client = state)
                using (NetworkStream stream = client.GetStream())
                {
                    this.Send(stream, Message.Hello);
                    var cmd = (Message)stream.ReadByte();

                    if (cmd == Message.Hello)
                    {
                        this.Send(stream, new Hello {Version = Hello.VersionNumber});

                        var hello = this.Get<Hello>(stream);
                        if (hello.Version != Hello.VersionNumber)
                        {
                            throw new InvalidDataException("Server and Client version numbers do not match.");
                        }

                        callback(client, stream);
                    }
                    else if (cmd == Message.RequestDebug)
                    {
                        this.OnDebugRequest(stream);
                    }
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
            this.Send(stream, message);
            this.Send(stream, messageObj);
        }

        public void Send<T>(NetworkStream stream, T messageObj)
        {
            Serializer.SerializeWithLengthPrefix(stream, messageObj, PrefixStyle.Base128);
        }
    }
}