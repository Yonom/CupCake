using CupCake.Messages;
using CupCake.Messages.Blocks;

namespace CupCake.Messages
{
    public class KeyTrigger
    {
        /// <summary>
        ///     Gets or sets the key
        /// </summary>
        /// <value>The key.</value>
        public Key Key { get; set; }

        /// <summary>
        /// Creates a key trigger
        /// </summary>
        /// <param name="key">Key pressed</param>
        public KeyTrigger(Key key)
        {
            Key = key;
        }
    }
}
