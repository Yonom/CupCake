using System;
using System.ComponentModel;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public abstract class ReceiveMessage : EventArgs
    {
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public readonly Message PlayerIOMessage;

        internal ReceiveMessage(Message message)
        {
            this.PlayerIOMessage = message;
        }
    }
}