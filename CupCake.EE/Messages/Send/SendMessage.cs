using System;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public abstract class SendMessage : EventArgs
    {
        internal abstract Message GetMessage();
    }
}