using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public sealed class InitSendMessage : SendMessage
    {
        //No arguments

        internal override Message GetMessage()
        {
            return Message.Create("init");
        }
    }
}