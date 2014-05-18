using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public sealed class MoveSendMessage : SendMessage
    {
        public readonly double GravityMultiplier;
        public readonly double Horizontal;
        public readonly double ModifierX;
        public readonly double ModifierY;
        public readonly int PosX;
        public readonly int PosY;
        public readonly double SpeedX;
        public readonly double SpeedY;
        public readonly double Vertical;

        public MoveSendMessage(int posX, int posY, double speedX, double speedY, double modifierX, double modifierY,
            double horizontal, double vertical, double gravityMultiplier)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.SpeedX = speedX;
            this.SpeedY = speedY;
            this.ModifierX = modifierX;
            this.ModifierY = modifierY;
            this.Horizontal = horizontal;
            this.Vertical = vertical;
            this.GravityMultiplier = gravityMultiplier;
        }

        internal override Message GetMessage()
        {
            return Message.Create("m", this.PosX, this.PosY, this.SpeedX, this.SpeedY, this.ModifierX, this.ModifierY,
                this.Horizontal, this.Vertical, this.GravityMultiplier);
        }
    }
}