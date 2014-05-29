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
            this._listener = new CupCakeListener(ipAddress, port, (c, s) =>
            {
                var handle = new ClientHandle();
                onConnection(handle);
                this.MessageLoop(handle, c, s);
            });
        }

        public void Connect(IPEndPoint endPoint, Action<ClientHandle> onConnection)
        {
            this._listener.Connect(endPoint, (c, s) =>
            {
                var handle = new ClientHandle();
                onConnection(handle);
                this.MessageLoop(handle, c, s);
            });
        }

        private void MessageLoop(ClientHandle handle, TcpClient client, NetworkStream stream)
        {
            Action<Authentication> onAuthentication = m => this.Send(stream, Message.Authentication, m);
            Action<Input> onInput = m => this.Send(stream, Message.Input, m);
            Action<Output> onOutput = m => this.Send(stream, Message.Output, m);
            Action<Title> onTitle = m => this.Send(stream, Message.Title, m);
            Action<SetData> onSetData = m => this.Send(stream, Message.SetData, m);
            Action onWrongAuth = () => this.Send(stream, Message.WrongAuth);
            Action onClose = client.Close;

            handle.SendAuthentication += onAuthentication;
            handle.SendInput += onInput;
            handle.SendOutput += onOutput;
            handle.SendTitle += onTitle;
            handle.SendSetData += onSetData;
            handle.SendClose += onClose;
            handle.SendWrongAuth += onWrongAuth;

            try
            {
                bool closing = false;
                while (!closing)
                {
                    var messageId = (Message)stream.ReadByte();
                    switch (messageId)
                    {
                        case Message.Authentication:
                            var auth = this._listener.Get<Authentication>(stream);
                            handle.DoReceiveAuthentication(auth);
                            break;

                        case Message.Input:
                            var input = this._listener.Get<Input>(stream);
                            handle.DoReceiveInput(input);
                            break;

                        case Message.Output:
                            var output = this._listener.Get<Output>(stream);
                            handle.DoReceiveOutput(output);
                            break;

                        case Message.Title:
                            var title = this._listener.Get<Title>(stream);
                            handle.DoReceiveTitle(title);
                            break;

                        case Message.SetData:
                            var data = this._listener.Get<SetData>(stream);
                            handle.DoReceiveSetData(data);
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
                handle.SendAuthentication -= onAuthentication;
                handle.SendInput -= onInput;
                handle.SendOutput -= onOutput;
                handle.SendTitle -= onTitle;
                handle.SendSetData -= onSetData;
                handle.SendClose -= onClose;

                handle.DoReceiveClose();
            }
        }

        private void Send(NetworkStream stream, Message message)
        {
            try
            {
                this._listener.Send(stream, message);
            }
            catch (IOException ex)
            {
                Debug.WriteLine("Error while writing: " + ex.Message);
            }
        }

        private void Send<T>(NetworkStream stream, Message message, T messageObj)
        {
            try
            {
                this._listener.Send(stream, message, messageObj);
            }
            catch (IOException ex)
            {
                Debug.WriteLine("Error while writing: " + ex.Message);
            }
        }
    }
}
