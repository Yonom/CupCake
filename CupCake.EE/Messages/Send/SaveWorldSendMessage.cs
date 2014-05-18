using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class SaveWorldSendMessage : SendMessage
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("save");
        }
    }
}