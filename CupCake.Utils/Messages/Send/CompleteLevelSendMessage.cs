using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
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