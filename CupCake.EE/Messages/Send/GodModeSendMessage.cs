using PlayerIOClient;

public sealed class GodModeSendMessage : SendMessage
{
    public readonly bool GodModeEnabled;

    public GodModeSendMessage(bool godModeEnabled)
    {
        this.GodModeEnabled = godModeEnabled;
    }

    internal override Message GetMessage()
    {
        return Message.Create("god", this.GodModeEnabled);
    }
}