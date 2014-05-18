using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
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