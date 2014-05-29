using System;

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

        protected virtual void OnReceiveInput(Input obj)
        {
            Action<Input> handler = this.ReceiveInput;
            if (handler != null) handler(obj);
        }

        internal event Action<Input> SendInput;

        private void OnSendInput(Input obj)
        {
            Action<Input> handler = this.SendInput;
            if (handler != null) handler(obj);
        }

        public event Action<Output> ReceiveOutput;

        protected virtual void OnReceiveOutput(Output obj)
        {
            Action<Output> handler = this.ReceiveOutput;
            if (handler != null) handler(obj);
        }

        internal event Action<Output> SendOutput;

        private void OnSendOutput(Output obj)
        {
            Action<Output> handler = this.SendOutput;
            if (handler != null) handler(obj);
        }

        public event Action<SetData> ReceiveSetData;

        protected virtual void OnReceiveSetData(SetData obj)
        {
            Action<SetData> handler = this.ReceiveSetData;
            if (handler != null) handler(obj);
        }

        internal event Action<SetData> SendSetData;

        private void OnSendSetData(SetData obj)
        {
            Action<SetData> handler = this.SendSetData;
            if (handler != null) handler(obj);
        }

        public event Action<Authentication> ReceiveAuthentication;

        protected virtual void OnReceiveAuthentication(Authentication obj)
        {
            Action<Authentication> handler = this.ReceiveAuthentication;
            if (handler != null) handler(obj);
        }

        internal event Action<Authentication> SendAuthentication;

        private void OnSendAuthentication(Authentication obj)
        {
            Action<Authentication> handler = this.SendAuthentication;
            if (handler != null) handler(obj);
        }

        public event Action<Title> ReceiveTitle;

        protected virtual void OnReceiveTitle(Title obj)
        {
            Action<Title> handler = this.ReceiveTitle;
            if (handler != null) handler(obj);
        }

        internal event Action<Title> SendTitle;

        private void OnSendTitle(Title obj)
        {
            Action<Title> handler = this.SendTitle;
            if (handler != null) handler(obj);
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


        internal void DoReceiveSetData(SetData data)
        {
            this.OnReceiveSetData(data);
        }

        public void DoSendSetData(string email, string password, string worldId, string[] directories)
        {
            this.OnSendSetData(new SetData { Email = email, Password = password, World = worldId, Directories = directories });
        }

        internal void DoReceiveInput(Input input)
        {
            this.OnReceiveInput(input);
        }

        public void DoSendInput(string text)
        {
            this.OnSendInput(new Input { Text = text });
        }

        internal void DoReceiveOutput(Output output)
        {
            this.OnReceiveOutput(output);
        }

        public void DoSendOutput(string text)
        {
            this.OnSendOutput(new Output { Text = text });
        }

        internal void DoReceiveTitle(Title title)
        {
            this.OnReceiveTitle(title);
        }

        public void DoSendTitle(string text)
        {
            this.OnSendTitle(new Title { Text = text });
        }

        internal void DoReceiveAuthentication(Authentication auth)
        {
            this.OnReceiveAuthentication(auth);
        }

        public void DoSendAuthentication(string pin)
        {
            this.OnSendAuthentication(new Authentication { Pin = pin });
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
    }
}
