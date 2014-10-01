using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when a player is teleported to another location.
    /// </summary>
    public class TeleportUserReceiveEvent : ReceiveEvent, IUserPosReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public TeleportUserReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.UserPosX = message.GetInteger(1);
            this.UserPosY = message.GetInteger(2);
        }

        /// <summary>
        ///     Gets the block x.
        /// </summary>
        /// <value>The block x.</value>
        public int BlockX
        {
            get { return this.UserPosX + 8 >> 4; }
        }

        /// <summary>
        ///     Gets the block y.
        /// </summary>
        /// <value>The block y.</value>
        public int BlockY
        {
            get { return this.UserPosY + 8 >> 4; }
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