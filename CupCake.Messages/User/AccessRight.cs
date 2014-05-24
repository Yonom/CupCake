namespace CupCake.Messages.User
{
    /// <summary>
    ///     Represents the rights of the bot connection in the world.
    /// </summary>
    public enum AccessRight
    {
        /// <summary>
        ///     Represents the state where the bot doesn't have any rights in the world.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Represents the state where the bot has edit rights in the world.
        /// </summary>
        Edit = 1,

        /// <summary>
        ///     Represents the state where bot has command access and edit rights in the world.
        /// </summary>
        Owner = 2
    }
}