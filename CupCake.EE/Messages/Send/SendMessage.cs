using System;
using PlayerIOClient;

public abstract class SendMessage : EventArgs
{
    internal abstract Message GetMessage();
}