using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class WootUpSendMessage : SendMessage
    {
        //No arguments

        internal override Message GetMessage()
        {
            return Message.Create("wootup");
        }
    }
}