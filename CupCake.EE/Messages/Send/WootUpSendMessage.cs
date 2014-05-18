using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class WootUpSendMessage : SendMessage
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("wootup");
        }
    }
}