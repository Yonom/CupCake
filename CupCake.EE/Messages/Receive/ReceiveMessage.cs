using System;
using System.ComponentModel;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public abstract class ReceiveMessage : EventArgs
    {
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Message PlayerIOMessage { get; private set; }

        protected ReceiveMessage(Message message)
        {
            this.PlayerIOMessage = message;
        }
    }
}