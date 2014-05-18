using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public class DeathSendMessage : SendMessage
    {
        //No arguments

        internal override Message GetMessage()
        {
            return Message.Create("death");
        }
    }
}