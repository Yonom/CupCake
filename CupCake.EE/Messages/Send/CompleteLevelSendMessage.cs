using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class CompleteLevelSendMessage : SendMessage
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("levelcomplete");
        }
    }
}