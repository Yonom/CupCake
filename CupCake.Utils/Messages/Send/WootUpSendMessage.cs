using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
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