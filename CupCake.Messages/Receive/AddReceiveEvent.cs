using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Add Receive Event.
    /// </summary>
    public class AddReceiveEvent : ReceiveEvent, IUserPosReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The EE message.</param>
        public AddReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Username = message.GetString(1);
            this.Face = (Smiley)message.GetInteger(2);
            this.UserPosX = message.GetInteger(3);
            this.UserPosY = message.GetInteger(4);
            this.IsGod = message.GetBoolean(5);
            this.IsMod = message.GetBoolean(6);
            this.HasChat = message.GetBoolean(7);
            this.Coins = message.GetInteger(8);
            this.IsMyFriend = message.GetBoolean(9);
            this.IsPurple = message.GetBoolean(10);
            this.MagicClass = (MagicClass)message.GetInteger(11);
            this.IsClubMember = message.GetBoolean(12);
            this.IsGuardian = message.GetBoolean(13);
        }

        /// <summary>
        /// Whether the user is in guardian mode or not.
        /// </summary>
        /// <value><c>true</c> if the player has activated guardian mode; otherwise, <c>false</c>.</value>
        public bool IsGuardian { get; set; }
        /// <summary>
        /// Returns the amount of coins the player has.
        /// </summary>
        /// <value>The coins.</value>
        public int Coins { get; set; }
        /// <summary>
        /// Returns the smiley the player has.
        /// </summary>
        /// <value>The face.</value>
        public Smiley Face { get; set; }
        /// <summary>
        /// Returns whether this player may chat using the free-form chat box.
        /// </summary>
        /// <value><c>true</c> if this player has chat; otherwise, <c>false</c>.</value>
        public bool HasChat { get; set; }
        /// <summary>
        /// Returns whether this player is a club member.
        /// </summary>
        /// <value><c>true</c> if this player is a club member; otherwise, <c>false</c>.</value>
        public bool IsClubMember { get; set; }
        /// <summary>
        /// Returns whether this player has activated god mode.
        /// </summary>
        /// <value><c>true</c> if this player is in god mode; otherwise, <c>false</c>.</value>
        public bool IsGod { get; set; }
        /// <summary>
        /// Returns whether this player is a moderator.
        /// </summary>
        /// <value><c>true</c> if this player is a moderator; otherwise, <c>false</c>.</value>
        public bool IsMod { get; set; }
        /// <summary>
        /// Returns whether this player is my friend or not.
        /// </summary>
        /// <value><c>true</c> if this player is my friend; otherwise, <c>false</c>.</value>
        public bool IsMyFriend { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is purple.?
        /// </summary>
        /// <value><c>true</c> if this instance is purple; otherwise, <c>false</c>.</value>
        public bool IsPurple { get; set; }
        /// <summary>
        /// Returns the magic class of this player.
        /// </summary>
        /// <value>The magic class.</value>
        public MagicClass MagicClass { get; set; }
        /// <summary>
        /// Returns the username of the player.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }
        /// <summary>
        /// Returns the x coordinate of the player.
        /// </summary>
        /// <value>The user position x.</value>
        public int UserPosX { get; set; }
        /// <summary>
        /// Returns the y coordinate of the player.
        /// </summary>
        /// <value>The user position y.</value>
        public int UserPosY { get; set; }
        /// <summary>
        /// Returns the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}