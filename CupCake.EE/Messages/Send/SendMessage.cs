using System;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public abstract class SendMessage : EventArgs
    {
        public abstract Message GetMessage();
    }
}