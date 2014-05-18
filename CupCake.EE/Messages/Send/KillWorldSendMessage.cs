using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class KillWorldSendMessage : SendMessage
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("kill");
        }
    }
}