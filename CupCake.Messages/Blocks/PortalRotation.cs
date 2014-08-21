namespace CupCake.Messages.Blocks
{
    /// <summary>
    ///     Describes the direction of a portal.
    /// </summary>
    public class PortalRotation : uint
    {
        public const int
            /// <summary>
            ///     Portal pointing downwards
            /// </summary>
        Down = 0,

        /// <summary>
            ///     Portal pointing to the left
            /// </summary>
        Left = 1,

        /// <summary>
            ///     Portal pointing upwards
            /// </summary>
        Up = 2,

        /// <summary>
            ///     Portal pointing to the right
            /// </summary>
        Right = 3;
    }
}