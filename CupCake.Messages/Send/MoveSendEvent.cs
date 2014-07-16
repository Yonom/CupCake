using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class MoveSendEvent : SendEvent
    {
        public MoveSendEvent(int posX, int posY, double speedX, double speedY, double modifierX, double modifierY,
            double horizontal, double vertical, double gravityMultiplier, bool spaceDown)
        {
            PosX = posX;
            PosY = posY;
            SpeedX = speedX;
            SpeedY = speedY;
            ModifierX = modifierX;
            ModifierY = modifierY;
            Horizontal = horizontal;
            Vertical = vertical;
            GravityMultiplier = gravityMultiplier;
            SpaceDown = spaceDown;
        }

        public double GravityMultiplier { get; set; }
        public bool SpaceDown { get; set; }
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
            return Message.Create("m", PosX, PosY, SpeedX, SpeedY, ModifierX, ModifierY,
                Horizontal, Vertical, GravityMultiplier, SpaceDown);
        }
    }
}