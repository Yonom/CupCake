using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class MoveSendEvent : SendEvent
    {
        public MoveSendEvent(int posX, int posY, double speedX, double speedY, double modifierX, double modifierY,
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

        public double GravityMultiplier { get; set; }
        public double Horizontal { get; set; }
        public double ModifierX { get; set; }
        public double ModifierY { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }
        public double Vertical { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("m", this.PosX, this.PosY, this.SpeedX, this.SpeedY, this.ModifierX, this.ModifierY,
                this.Horizontal, this.Vertical, this.GravityMultiplier);
        }
    }
}