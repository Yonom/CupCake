using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Move Send Event
    /// </summary>
    public class MoveSendEvent : SendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MoveSendEvent" /> class.
        /// </summary>
        /// <param name="posX">The x-coordinate of the position.</param>
        /// <param name="posY">The y-coordinate of the position.</param>
        /// <param name="speedX">The horizontal speed.</param>
        /// <param name="speedY">The vertical speed.</param>
        /// <param name="modifierX">The horizontal speed modifier.</param>
        /// <param name="modifierY">The vertical speed modifier.</param>
        /// <param name="horizontal">The horizontal speed direction.</param>
        /// <param name="vertical">The vertical speed direction.</param>
        /// <param name="gravityMultiplier">The gravity multiplier.</param>
        /// <param name="spaceDown">if set to <c>true</c> then spacebar is pressed.</param>
        public MoveSendEvent(int posX, int posY, double speedX, double speedY, double modifierX, double modifierY,
            double horizontal, double vertical, double gravityMultiplier, bool spaceDown)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.SpeedX = speedX;
            this.SpeedY = speedY;
            this.ModifierX = modifierX;
            this.ModifierY = modifierY;
            this.Horizontal = horizontal;
            this.Vertical = vertical;
            this.SpaceDown = spaceDown;
        }

        /// <summary>
        ///     Gets or sets the gravity multiplier.
        /// </summary>
        /// <value>
        ///     The gravity multiplier.
        /// </value>
        public double GravityMultiplier { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether spacebar is pressed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if spacebar is pressed; otherwise, <c>false</c>.
        /// </value>
        public bool SpaceDown { get; set; }

        /// <summary>
        ///     Gets or sets the horizontal speed direction.
        /// </summary>
        /// <value>
        ///     The horizontal speed direction.
        /// </value>
        public double Horizontal { get; set; }

        /// <summary>
        ///     Gets or sets the vertical speed direction.
        /// </summary>
        /// <value>
        ///     The vertical speed direction.
        /// </value>
        public double Vertical { get; set; }

        /// <summary>
        ///     Gets or sets the horizontal speed modifier.
        /// </summary>
        /// <value>
        ///     The horizontal speed modifier.
        /// </value>
        public double ModifierX { get; set; }

        /// <summary>
        ///     Gets or sets the vertical speed modifier.
        /// </summary>
        /// <value>
        ///     The vertical speed modifier.
        /// </value>
        public double ModifierY { get; set; }

        /// <summary>
        ///     Gets or sets the x-coordinate of the position.
        /// </summary>
        /// <value>
        ///     The x-coordinate of the position.
        /// </value>
        public int PosX { get; set; }

        /// <summary>
        ///     Gets or sets the y-coordinate of the position.
        /// </summary>
        /// <value>
        ///     The y-coordinate of the position.
        /// </value>
        public int PosY { get; set; }

        /// <summary>
        ///     Gets or sets the horizontal speed.
        /// </summary>
        /// <value>
        ///     The horizontal speed.
        /// </value>
        public double SpeedX { get; set; }

        /// <summary>
        ///     Gets or sets the vertical speed.
        /// </summary>
        /// <value>
        ///     The vertical speed.
        /// </value>
        public double SpeedY { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("m", this.PosX, this.PosY, this.SpeedX, this.SpeedY, this.ModifierX, this.ModifierY,
                this.Horizontal, this.Vertical, this.SpaceDown, this.SpaceDown, 0);
        }
    }
}