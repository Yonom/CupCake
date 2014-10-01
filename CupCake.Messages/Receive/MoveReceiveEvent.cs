using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when a player moves.
    /// </summary>
    public class MoveReceiveEvent : ReceiveEvent, IUserPosReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MoveReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MoveReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.UserPosX = message.GetInteger(1);
            this.UserPosY = message.GetInteger(2);
            this.SpeedX = message.GetDouble(3);
            this.SpeedY = message.GetDouble(4);
            this.ModifierX = message.GetDouble(5);
            this.ModifierY = message.GetDouble(6);
            this.Horizontal = message.GetDouble(7);
            this.Vertical = message.GetDouble(8);
            this.Coins = message.GetInteger(9);
            this.IsPurple = message.GetBoolean(10);
            this.SpaceDown = message.GetBoolean(11);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the player is holding down the space bar.
        /// </summary>
        /// <value><c>true</c> if the space bar is held down; otherwise, <c>false</c>.</value>
        public bool SpaceDown { get; set; }

        /// <summary>
        ///     Gets or sets the coins.
        /// </summary>
        /// <value>The coins.</value>
        public int Coins { get; set; }

        /// <summary>
        ///     Gets or sets the horizontal.
        /// </summary>
        /// <value>The horizontal.</value>
        public double Horizontal { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the player toggled a purple switch.
        /// </summary>
        /// <value><c>true</c> if the player toggled the purple switch; otherwise, <c>false</c>.</value>
        public bool IsPurple { get; set; }

        /// <summary>
        ///     Gets or sets the x modifier.
        /// </summary>
        /// <value>The modifier x.</value>
        public double ModifierX { get; set; }

        /// <summary>
        ///     Gets or sets the y modifier.
        /// </summary>
        /// <value>The modifier y.</value>
        public double ModifierY { get; set; }

        /// <summary>
        ///     Gets or sets the speed x.
        /// </summary>
        /// <value>The speed x.</value>
        public double SpeedX { get; set; }

        /// <summary>
        ///     Gets or sets the speed y.
        /// </summary>
        /// <value>The speed y.</value>
        public double SpeedY { get; set; }

        /// <summary>
        ///     Gets or sets the vertical.
        /// </summary>
        /// <value>The vertical.</value>
        public double Vertical { get; set; }

        /// <summary>
        ///     Gets the block x.
        /// </summary>
        /// <value>The block x.</value>
        public int BlockX
        {
            get { return BlockUtils.PosToBlock(this.UserPosX); }
        }

        /// <summary>
        ///     Gets the block y.
        /// </summary>
        /// <value>The block y.</value>
        public int BlockY
        {
            get { return BlockUtils.PosToBlock(this.UserPosY); }
        }

        /// <summary>
        ///     Gets or sets the user coordinate x.
        /// </summary>
        /// <value>The user position x.</value>
        public int UserPosX { get; set; }

        /// <summary>
        ///     Gets or sets the user coordinate y.
        /// </summary>
        /// <value>The user position y.</value>
        public int UserPosY { get; set; }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}