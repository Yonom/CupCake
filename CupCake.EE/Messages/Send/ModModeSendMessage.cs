using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class ModModeSendMessage : SendMessage
    {
        //No arguments

        internal override Message GetMessage()
        {
            return Message.Create("mod");
        }
    }
}