using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class SaveWorldSendMessage : SendMessage
    {
        //No arguments

        internal override Message GetMessage()
        {
            return Message.Create("save");
        }
    }
}