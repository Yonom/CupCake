namespace CupCake.Messages.Blocks
{
    /// <summary>
    ///     Describes the layer where a block is located on.
    /// </summary>
    public class Layer
    {
        /// <summary>
        ///     The foreground layer (contains solid, action, and decoration blocks).
        ///     The background layer (contains background blocks).
        /// </summary>
        public const int
            Foreground = 0,
            Background = 1;
    }
}