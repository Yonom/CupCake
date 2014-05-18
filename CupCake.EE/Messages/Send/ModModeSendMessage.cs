using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class ModModeSendMessage : SendMessage
    {
        //No arguments

        public override Message GetMessage()
        {
            return Message.Create("mod");
        }
    }
}