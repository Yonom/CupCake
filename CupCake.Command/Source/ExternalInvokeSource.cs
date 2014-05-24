using System;

namespace CupCake.Command.Source
{
    public class ExternalInvokeSource : IInvokeSource
    {
        private readonly Action<string> _onReply;

        public ExternalInvokeSource(string name, Action<string> onReply)
        {
            this.Name = name;
            this._onReply = onReply;
        }

        public string Name { get; set; }

        public bool Handled { get; set; }

        public void Reply(string message)
        {
            this._onReply(message);
        }
    }
}