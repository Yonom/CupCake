using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class TeleportUserReceiveEvent : ReceiveEvent, IUserPosReceiveEvent
    {
        public TeleportUserReceiveEvent(Message message)
            : base(message)
        {
            UserId = message.GetInteger(0);
            UserPosX = message.GetInteger(1);
            UserPosY = message.GetInteger(2);
        }

        public int BlockX
        {
            get { return UserPosX + 8 >> 4; }
        }

        public int BlockY
        {
            get { return UserPosY + 8 >> 4; }
        }

        public int UserPosX { get; set; }
        public int UserPosY { get; set; }
        public int UserId { get; set; }
    }
}