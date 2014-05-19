using System;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public abstract class SendMessage : EventArgs
    {
        public bool Cancelled { get; set; }

        public abstract Message GetMessage();
    }
}