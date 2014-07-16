using System;

namespace CupCake.Protocol
{
    public class ClientHandle
    {
        internal event Action SendClose;

        private void OnSendClose()
        {
            Action handler = SendClose;
            if (handler != null) handler();
        }

        public event Action ReceiveClose;

        protected virtual void OnReceiveClose()
        {
            Action handler = ReceiveClose;
            if (handler != null) handler();
        }

        public event Action<Input> ReceiveInput;

        protected virtual void OnReceiveInput(Input input)
        {
            Action<Input> handler = ReceiveInput;
            if (handler != null) handler(input);
        }

        internal event Action<Input> SendInput;

        private void OnSendInput(Input input)
        {
            Action<Input> handler = SendInput;
            if (handler != null) handler(input);
        }

        public event Action<Output> ReceiveOutput;

        protected virtual void OnReceiveOutput(Output output)
        {
            Action<Output> handler = ReceiveOutput;
            if (handler != null) handler(output);
        }

        internal event Action<Output> SendOutput;

        private void OnSendOutput(Output output)
        {
            Action<Output> handler = SendOutput;
            if (handler != null) handler(output);
        }

        public event Action<SetData> ReceiveSetData;

        protected virtual void OnReceiveSetData(SetData setData)
        {
            Action<SetData> handler = ReceiveSetData;
            if (handler != null) handler(setData);
        }

        internal event Action<SetData> SendSetData;

        private void OnSendSetData(SetData setData)
        {
            Action<SetData> handler = SendSetData;
            if (handler != null) handler(setData);
        }

        public event Action<Authentication> ReceiveAuthentication;

        protected virtual void OnReceiveAuthentication(Authentication auth)
        {
            Action<Authentication> handler = ReceiveAuthentication;
            if (handler != null) handler(auth);
        }

        internal event Action<Authentication> SendAuthentication;

        private void OnSendAuthentication(Authentication auth)
        {
            Action<Authentication> handler = SendAuthentication;
            if (handler != null) handler(auth);
        }

        public event Action<Title> ReceiveTitle;

        protected virtual void OnReceiveTitle(Title title)
        {
            Action<Title> handler = ReceiveTitle;
            if (handler != null) handler(title);
        }

        internal event Action<Title> SendTitle;

        private void OnSendTitle(Title title)
        {
            Action<Title> handler = SendTitle;
            if (handler != null) handler(title);
        }

        public event Action ReceiveWrongAuth;

        protected virtual void OnReceiveWrongAuth()
        {
            Action handler = ReceiveWrongAuth;
            if (handler != null) handler();
        }

        internal event Action SendWrongAuth;

        private void OnSendWrongAuth()
        {
            Action handler = SendWrongAuth;
            if (handler != null) handler();
        }

        public event Action<RequestData> ReceiveRequestData;

        protected virtual void OnReceiveRequestData(RequestData reqData)
        {
            Action<RequestData> handler = ReceiveRequestData;
            if (handler != null) handler(reqData);
        }

        internal event Action<RequestData> SendRequestData;

        private void OnSendRequestData(RequestData reqData)
        {
            Action<RequestData> handler = SendRequestData;
            if (handler != null) handler(reqData);
        }

        public event Action<Status> ReceiveStatus;

        protected virtual void OnReceiveStatus(Status reqData)
        {
            Action<Status> handler = ReceiveStatus;
            if (handler != null) handler(reqData);
        }

        internal event Action<Status> SendStatus;

        private void OnSendStatus(Status reqData)
        {
            Action<Status> handler = SendStatus;
            if (handler != null) handler(reqData);
        }


        internal void DoReceiveSetData(SetData data)
        {
            OnReceiveSetData(data);
        }

        public void DoSendSetData(AccountType accountType, string email, string password, string worldId,
            string[] directories, DatabaseType dbType, string cs)
        {
            OnSendSetData(new SetData
            {
                AccountType = accountType,
                Email = email,
                Password = password,
                World = worldId,
                Directories = directories,
                DatabaseType = dbType,
                ConnectionString = cs
            });
        }

        internal void DoReceiveInput(Input input)
        {
            OnReceiveInput(input);
        }

        public void DoSendInput(string text)
        {
            OnSendInput(new Input {Text = text});
        }

        internal void DoReceiveOutput(Output output)
        {
            OnReceiveOutput(output);
        }

        public void DoSendOutput(string text)
        {
            OnSendOutput(new Output {Text = text});
        }

        internal void DoReceiveTitle(Title title)
        {
            OnReceiveTitle(title);
        }

        public void DoSendTitle(string text)
        {
            OnSendTitle(new Title {Text = text});
        }

        internal void DoReceiveAuthentication(Authentication auth)
        {
            OnReceiveAuthentication(auth);
        }

        public void DoSendAuthentication(string pin)
        {
            OnSendAuthentication(new Authentication {Pin = pin});
        }

        internal void DoReceiveClose()
        {
            OnReceiveClose();
        }

        public void DoSendClose()
        {
            OnSendClose();
        }

        public void DoSendWrongAuth()
        {
            OnSendWrongAuth();
        }

        internal void DoReceiveWrongAuth()
        {
            OnReceiveWrongAuth();
        }

        internal void DoReceiveRequestData(RequestData reqData)
        {
            OnReceiveRequestData(reqData);
        }

        public void DoSendRequestData(bool debug)
        {
            OnSendRequestData(new RequestData {IsDebug = debug});
        }

        internal void DoReceiveStatus(Status reqData)
        {
            OnReceiveStatus(reqData);
        }

        public void DoSendStatus(string text)
        {
            OnSendStatus(new Status {Text = text});
        }
    }
}