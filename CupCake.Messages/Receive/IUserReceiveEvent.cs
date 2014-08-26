namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Interface IUserReceiveEvent
    /// </summary>
    public interface IUserReceiveEvent
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        int UserId { get; set; }
    }
}