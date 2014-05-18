using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class WriteReceiveMessage : ReceiveMessage
    {
        //0
        //1

        public readonly string Text;
        public readonly string Title;

        internal WriteReceiveMessage(Message message)
            : base(message)
        {
            this.Title = message.GetString(0);
            this.Text = message.GetString(1);
        }
    }
}