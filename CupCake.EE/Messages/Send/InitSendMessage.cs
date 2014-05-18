using PlayerIOClient;

namespace CupCake.EE.Messages.Send
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