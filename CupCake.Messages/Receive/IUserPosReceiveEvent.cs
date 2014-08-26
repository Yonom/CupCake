namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Interface IUserPosReceiveEvent
    /// </summary>
    public interface IUserPosReceiveEvent : IUserReceiveEvent
    {
        /// <summary>
        /// Gets or sets the user coordinate x.
        /// </summary>
        /// <value>The user position x.</value>
        int UserPosX { get; set; }
        /// <summary>
        /// Gets or sets the user coordinate y.
        /// </summary>
        /// <value>The user position y.</value>
        int UserPosY { get; set; }
    }
}