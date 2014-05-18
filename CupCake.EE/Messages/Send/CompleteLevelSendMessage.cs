using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class CompleteLevelSendMessage : SendMessage
    {
        //No arguments

        internal override Message GetMessage()
        {
            return Message.Create("levelcomplete");
        }
    }
}