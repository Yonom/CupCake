using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class DeathSendMessage : SendMessage
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("death");
        }
    }
}