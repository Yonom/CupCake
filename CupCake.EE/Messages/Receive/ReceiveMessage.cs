using System;
using System.ComponentModel;
using PlayerIOClient;

public abstract class ReceiveMessage : EventArgs
{
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public readonly Message PlayerIOMessage;

    internal ReceiveMessage(Message message)
    {
        this.PlayerIOMessage = message;
    }
}