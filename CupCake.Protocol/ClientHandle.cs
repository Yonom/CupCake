using System;
using System.Net;

namespace CupCake.Protocol
{
    public class ClientHandle
    {
        internal event Action SendClose;

        private void OnSendClose()
        {
            Action handler = this.SendClose;
            if (handler != null) handler();
        }

        public event Action ReceiveClose;

        protected virtual void OnReceiveClose()
        {
            Action handler = this.ReceiveClose;
            if (handler != null) handler();
        }

        public event Action<Input> ReceiveInput;

        protected virtual void OnReceiveInput(Input input)
        {
            Action<Input> handler = this.ReceiveInput;
            if (handler != null) handler(input);
        }

        internal event Action<Input> SendInput;

        private void OnSendInput(Input input)
        {
            Action<Input> handler = this.SendInput;
            if (handler != null) handler(input);
        }

        public event Action<Output> ReceiveOutput;

        protected virtual void OnReceiveOutput(Output output)
        {
            Action<Output> handler = this.ReceiveOutput;
            if (handler != null) handler(output);
        }

        internal event Action<Output> SendOutput;

        private void OnSendOutput(Output output)
        {
            Action<Output> handler = this.SendOutput;
            if (handler != null) handler(output);
        }

        public event Action<SetData> ReceiveSetData;

        protected virtual void OnReceiveSetData(SetData setData)
        {
            Action<SetData> handler = this.ReceiveSetData;
            if (handler != null) handler(setData);
        }

        internal event Action<SetData> SendSetData;

        private void OnSendSetData(SetData setData)
        {
            Action<SetData> handler = this.SendSetData;
            if (handler != null) handler(setData);
        }

        public event Action<Authentication> ReceiveAuthentication;

        protected virtual void OnReceiveAuthentication(Authentication auth)
        {
            Action<Authentication> handler = this.ReceiveAuthentication;
            if (handler != null) handler(auth);
        }

        internal event Action<Authentication> SendAuthentication;

        private void OnSendAuthentication(Authentication auth)
        {
            Action<Authentication> handler = this.SendAuthentication;
            if (handler != null) handler(auth);
        }

        public event Action<Title> ReceiveTitle;

        protected virtual void OnReceiveTitle(Title title)
        {
            Action<Title> handler = this.ReceiveTitle;
            if (handler != null) handler(title);
        }

        internal event Action<Title> SendTitle;

        private void OnSendTitle(Title title)
        {
            Action<Title> handler = this.SendTitle;
            if (handler != null) handler(title);
        }

        public event Action ReceiveWrongAuth;

        protected virtual void OnReceiveWrongAuth()
        {
            Action handler = this.ReceiveWrongAuth;
            if (handler != null) handler();
        }

        internal event Action SendWrongAuth;

        private void OnSendWrongAuth()
        {
            Action handler = this.SendWrongAuth;
            if (handler != null) handler();
        }

        public event Action<RequestData> ReceiveRequestData;

        protected virtual void OnReceiveRequestData(RequestData reqData)
        {
            Action<RequestData> handler = this.ReceiveRequestData;
            if (handler != null) handler(reqData);
        }

        internal event Action<RequestData> SendRequestData;

        private void OnSendRequestData(RequestData reqData)
        {
            Action<RequestData> handler = this.SendRequestData;
            if (handler != null) handler(reqData);
        }

        public event Action<Status> ReceiveStatus;

        protected virtual void OnReceiveStatus(Status reqData)
        {
            Action<Status> handler = this.ReceiveStatus;
            if (handler != null) handler(reqData);
        }

        internal event Action<Status> SendStatus;

        private void OnSendStatus(Status reqData)
        {
            Action<Status> handler = this.SendStatus;
            if (handler != null) handler(reqData);
        }


        internal void DoReceiveSetData(SetData data)
        {
            this.OnReceiveSetData(data);
        }

        public void DoSendSetData(string email, string password, string worldId,
            string[] directories, DatabaseType dbType, string cs, string settings)
        {
            this.OnSendSetData(new SetData
            {
                Email = email,
                Password = password,
                World = worldId,
                Directories = directories,
                DatabaseType = dbType,
                ConnectionString = cs,
                Settings = settings
            });
        }

        internal void DoReceiveInput(Input input)
        {
            this.OnReceiveInput(input);
        }

        public void DoSendInput(string text)
        {
            this.OnSendInput(new Input {Text = text});
        }

        internal void DoReceiveOutput(Output output)
        {
            this.OnReceiveOutput(output);
        }

        public void DoSendOutput(string text)
        {
            this.OnSendOutput(new Output {Text = text});
        }

        internal void DoReceiveTitle(Title title)
        {
            this.OnReceiveTitle(title);
        }

        public void DoSendTitle(string text)
        {
            this.OnSendTitle(new Title {Text = text});
        }

        internal void DoReceiveAuthentication(Authentication auth)
        {
            this.OnReceiveAuthentication(auth);
        }

        public void DoSendAuthentication(string pin)
        {
            this.OnSendAuthentication(new Authentication {Pin = pin});
        }

        internal void DoReceiveClose()
        {
            this.OnReceiveClose();
        }

        public void DoSendClose()
        {
            this.OnSendClose();
        }

        public void DoSendWrongAuth()
        {
            this.OnSendWrongAuth();
        }

        internal void DoReceiveWrongAuth()
        {
            this.OnReceiveWrongAuth();
        }

        internal void DoReceiveRequestData(RequestData reqData)
        {
            this.OnReceiveRequestData(reqData);
        }

        public void DoSendRequestData(bool debug)
        {
            this.OnSendRequestData(new RequestData {IsDebug = debug});
        }

        internal void DoReceiveStatus(Status reqData)
        {
            this.OnReceiveStatus(reqData);
        }

        public void DoSendStatus(string text)
        {
            this.OnSendStatus(new Status {Text = text});
        }
    }
}