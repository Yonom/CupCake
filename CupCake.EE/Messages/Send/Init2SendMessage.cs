using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class Init2SendMessage : SendMessage
    {
        //No arguments

        internal override Message GetMessage()
        {
            return Message.Create("init2");
        }
    }
}