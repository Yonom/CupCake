namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Interface IMetadataReceiveMessage
    /// </summary>
    public interface IMetadataReceiveMessage
    {
        /// <summary>
        ///     Gets or sets the current woots of the world.
        /// </summary>
        /// <value>The current woots.</value>
        int CurrentWoots { get; set; }

        /// <summary>
        ///     Gets or sets the owner username of the world.
        /// </summary>
        /// <value>The owner username.</value>
        string OwnerUsername { get; set; }

        /// <summary>
        ///     Gets or sets the plays of the world.
        /// </summary>
        /// <value>The plays.</value>
        int Plays { get; set; }

        /// <summary>
        ///     Gets or sets the total woots of the world.
        /// </summary>
        /// <value>The total woots.</value>
        int TotalWoots { get; set; }

        /// <summary>
        ///     Gets or sets the name of the world.
        /// </summary>
        /// <value>The name of the world.</value>
        string WorldName { get; set; }
    }
}