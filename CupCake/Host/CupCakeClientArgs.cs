using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Host
{
    /// <summary>
    /// Class CupCakeClientArgs
    /// </summary>
    public class CupCakeClientArgs
    {
        /// <summary>
        /// Gets the world identifier.
        /// </summary>
        /// <value>
        /// The world identifier.
        /// </value>
        public string WorldId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CupCakeClientArgs"/> class.
        /// </summary>
        public CupCakeClientArgs()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CupCakeClientArgs"/> class.
        /// </summary>
        /// <param name="worldId">The world identifier.</param>
        public CupCakeClientArgs(string worldId)
        {
            this.WorldId = worldId;
        }
    }
}
