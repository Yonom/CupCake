using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class SayReceiveEvent : ReceiveEvent
    {
        public SayReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Text = message.GetString(1);
            this.IsMyFriend = message.GetBoolean(2);
        }

        public bool IsMyFriend { get; private set; }
        public string Text { get; private set; }
        public int UserId { get; private set; }
    }
}