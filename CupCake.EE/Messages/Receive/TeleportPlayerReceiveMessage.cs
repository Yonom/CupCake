using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class TeleportPlayerReceiveMessage : ReceiveMessage
    {
        public readonly int PlayerPosX;
        public readonly int PlayerPosY;
        public readonly int UserID;

        internal TeleportPlayerReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
            this.PlayerPosX = message.GetInteger(1);
            this.PlayerPosY = message.GetInteger(2);
        }

        public int BlockX
        {
            get { return this.PlayerPosX + 8 >> 4; }
        }

        public int BlockY
        {
            get { return this.PlayerPosY + 8 >> 4; }
        }
    }
}