using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when the player initially joins the room. Contains world information such as title and world content.
    /// </summary>
    public class InitReceiveEvent : ReceiveEvent, IUserPosReceiveEvent, IMetadataReceiveMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InitReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The EE message.</param>
        public InitReceiveEvent(Message message)
            : base(message)
        {
            this.OwnerUsername = message.GetString(0);
            this.WorldName = message.GetString(1);
            this.Plays = message.GetInteger(2);
            this.CurrentWoots = message.GetInteger(3);
            this.TotalWoots = message.GetInteger(4);
            this.Encryption = message.GetString(5);
            this.UserId = message.GetInteger(6);
            this.Face = (Smiley)message.GetInteger(7);
            // Aura
            this.SpawnX = message.GetInteger(9);
            this.SpawnY = message.GetInteger(10);
            this.ChatColor = message.GetUInt(11);
            this.Username = message.GetString(12);
            this.CanEdit = message.GetBoolean(13);
            this.IsOwner = message.GetBoolean(14);
            this.RoomWidth = message.GetInteger(15);
            this.RoomHeight = message.GetInteger(16);
            this.IsTutorialRoom = message.GetBoolean(17);
            this.Gravity = message.GetDouble(18);
        }

        public uint ChatColor { get; set; }

        public Smiley Face { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this player is allowed to edit.
        /// </summary>
        /// <value><c>true</c> if this instance can edit; otherwise, <c>false</c>.</value>
        public bool CanEdit { get; set; }

        /// <summary>
        ///     Gets or sets the encryption option of the world.
        /// </summary>
        /// <value>The encryption.</value>
        public string Encryption { get; set; }

        /// <summary>
        ///     Gets or sets the gravity of the world.
        /// </summary>
        /// <value>The gravity.</value>
        public double Gravity { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this player owns the world.
        /// </summary>
        /// <value><c>true</c> if this player is the owner; otherwise, <c>false</c>.</value>
        public bool IsOwner { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this world is a tutorial world.
        /// </summary>
        /// <value><c>true</c> if this world is a tutorial world; otherwise, <c>false</c>.</value>
        public bool IsTutorialRoom { get; set; }

        /// <summary>
        ///     Gets or sets the width of the world.
        /// </summary>
        /// <value>The width of the room.</value>
        public int RoomWidth { get; set; }

        /// <summary>
        ///     Gets or sets the height of the world.
        /// </summary>
        /// <value>The height of the room.</value>
        public int RoomHeight { get; set; }

        /// <summary>
        ///     Gets or sets the spawn x coordinate.
        /// </summary>
        /// <value>The spawn x.</value>
        public int SpawnX { get; set; }

        /// <summary>
        ///     Gets or sets the spawn y coordinate.
        /// </summary>
        /// <value>The spawn y.</value>
        public int SpawnY { get; set; }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        ///     Gets or sets the current woots of the world.
        /// </summary>
        /// <value>The current woots.</value>
        public int CurrentWoots { get; set; }

        /// <summary>
        ///     Gets or sets the owner username of the world.
        /// </summary>
        /// <value>The owner username.</value>
        public string OwnerUsername { get; set; }

        /// <summary>
        ///     Gets or sets the plays of the world.
        /// </summary>
        /// <value>The plays.</value>
        public int Plays { get; set; }

        /// <summary>
        ///     Gets or sets the total woots of the world.
        /// </summary>
        /// <value>The total woots.</value>
        public int TotalWoots { get; set; }

        /// <summary>
        ///     Gets or sets the name of the world.
        /// </summary>
        /// <value>The name of the world.</value>
        public string WorldName { get; set; }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the user coordinate x.
        /// </summary>
        /// <value>The user position x.</value>
        int IUserPosReceiveEvent.UserPosX
        {
            get { return this.SpawnX; }
            set { this.SpawnX = value; }
        }

        /// <summary>
        ///     Gets or sets the user coordinate y.
        /// </summary>
        /// <value>The user position y.</value>
        int IUserPosReceiveEvent.UserPosY
        {
            get { return this.SpawnY; }
            set { this.SpawnY = value; }
        }
    }
}