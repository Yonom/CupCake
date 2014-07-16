using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CupCake.Protocol
{
    public class ServerListener
    {
        public const int ServerPort = 4570;
        private readonly CupCakeListener _listener;

        public ServerListener(IPAddress ipAddress, int port, Action<ClientHandle> onConnection)
        {
            _listener = new CupCakeListener(ipAddress, port, (c, s) => Handleconnection(c, s, onConnection));
            _listener.PathRequest += OnPathRequest;
        }

        public event Action<Stream> PathRequest;

        protected virtual void OnPathRequest(Stream stream)
        {
            Action<Stream> handler = PathRequest;
            if (handler != null) handler(stream);
        }

        public void Connect(IPEndPoint endPoint, Action<ClientHandle> onConnection)
        {
            _listener.Connect(endPoint, (c, s) => Handleconnection(c, s, onConnection));
        }

        private void Handleconnection(TcpClient c, NetworkStream s, Action<ClientHandle> onConnection)
        {
            var handle = new ClientHandle();
            BindHandle(handle, c, s, () =>
            {
                onConnection(handle);
                MessageLoop(handle, c, s);
            });
        }

        private void BindHandle(ClientHandle handle, TcpClient client, NetworkStream stream, Action callback)
        {
            Action<Authentication> onAuthentication = m => Send(stream, Message.Authentication, m);
            Action<Input> onInput = m => Send(stream, Message.Input, m);
            Action<Output> onOutput = m => Send(stream, Message.Output, m);
            Action<Title> onTitle = m => Send(stream, Message.Title, m);
            Action<SetData> onSetData = m => Send(stream, Message.SetData, m);
            Action<RequestData> onRequestData = m => Send(stream, Message.RequestData, m);
            Action<Status> onStatus = m => Send(stream, Message.Status, m);
            Action onWrongAuth = () => Send(stream, Message.WrongAuth);
            Action onClose = client.Close;

            handle.SendAuthentication += onAuthentication;
            handle.SendInput += onInput;
            handle.SendOutput += onOutput;
            handle.SendTitle += onTitle;
            handle.SendSetData += onSetData;
            handle.SendRequestData += onRequestData;
            handle.SendStatus += onStatus;
            handle.SendWrongAuth += onWrongAuth;
            handle.SendClose += onClose;

            callback();

            handle.SendAuthentication -= onAuthentication;
            handle.SendInput -= onInput;
            handle.SendOutput -= onOutput;
            handle.SendTitle -= onTitle;
            handle.SendSetData -= onSetData;
            handle.SendRequestData -= onRequestData;
            handle.SendStatus -= onStatus;
            handle.SendWrongAuth -= onWrongAuth;
            handle.SendClose -= onClose;
        }

        private void MessageLoop(ClientHandle handle, TcpClient client, NetworkStream stream)
        {
            try
            {
                bool closing = false;
                while (!closing)
                {
                    var messageId = (Message) stream.ReadByte();
                    switch (messageId)
                    {
                        case Message.Authentication:
                            var auth = _listener.Get<Authentication>(stream);
                            handle.DoReceiveAuthentication(auth);
                            break;

                        case Message.Input:
                            var input = _listener.Get<Input>(stream);
                            handle.DoReceiveInput(input);
                            break;

                        case Message.Output:
                            var output = _listener.Get<Output>(stream);
                            handle.DoReceiveOutput(output);
                            break;

                        case Message.Title:
                            var title = _listener.Get<Title>(stream);
                            handle.DoReceiveTitle(title);
                            break;

                        case Message.SetData:
                            var data = _listener.Get<SetData>(stream);
                            handle.DoReceiveSetData(data);
                            break;

                        case Message.RequestData:
                            var reqData = _listener.Get<RequestData>(stream);
                            handle.DoReceiveRequestData(reqData);
                            break;

                        case Message.Status:
                            var status = _listener.Get<Status>(stream);
                            handle.DoReceiveStatus(status);
                            break;

                        case Message.WrongAuth:
                            handle.DoReceiveWrongAuth();
                            break;

                        default:
                            Debug.WriteLine("Received unknown message " + messageId + ", closing...");
                            closing = true;
                            break;
                    }
                }
            }
            finally
            {
                client.Close();
                handle.DoReceiveClose();
            }
        }

        private void Send(NetworkStream stream, Message message)
        {
            try
            {
                _listener.Send(stream, message);
            }
            catch (IOException ex)
            {
                Debug.WriteLine("Error while writing: " + ex.Message);
            }
        }

        private void Send<T>(NetworkStream stream, Message message, T messageObj)
        {
            if (stream.CanWrite)
            {
                try
                {
                    _listener.Send(stream, message, messageObj);
                }
                catch (IOException ex)
                {
                    Debug.WriteLine("Error while writing: " + ex.Message);
                }
            }
        }
    }
}