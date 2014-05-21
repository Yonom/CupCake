using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class TeleportUserReceiveEvent : ReceiveEvent, IUserPosEvent
    {
        public TeleportUserReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.UserPosX = message.GetInteger(1);
            this.UserPosY = message.GetInteger(2);
        }

        public int UserPosX { get; private set; }
        public int UserPosY { get; private set; }
        public int UserId { get; private set; }

        public int BlockX
        {
            get { return this.UserPosX + 8 >> 4; }
        }

        public int BlockY
        {
            get { return this.UserPosY + 8 >> 4; }
        }
    }
}