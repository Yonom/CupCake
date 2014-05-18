using System;
using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public abstract class SendMessage : EventArgs
    {
        internal abstract Message GetMessage();
    }
}