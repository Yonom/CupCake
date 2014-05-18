using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    internal sealed class CustomSendMessage : SendMessage
    {
        private readonly Message myMessage;

        public CustomSendMessage(string type, params string[] parameters)
        {
            this.myMessage = Message.Create(type, parameters);
        }

        internal override Message GetMessage()
        {
            return this.myMessage;
        }
    }
}