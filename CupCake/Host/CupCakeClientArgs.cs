using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using PlayerIOClient;

namespace CupCake.Host
{
    /// <summary>
    /// Class CupCakeClientArgs
    /// </summary>
    public class CupCakeClientArgs
    {
        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public Connection Connection { get; set; }

        /// <summary>
        /// Gets the world identifier.
        /// </summary>
        /// <value>
        /// The world identifier.
        /// </value>
        public string WorldId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CupCakeClientArgs" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        internal CupCakeClientArgs(Connection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            this.Connection = connection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CupCakeClientArgs" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="worldId">The world identifier.</param>
        public CupCakeClientArgs([NotNull]Connection connection, [NotNull]string worldId)
            : this(connection)
        {
            if (worldId == null)
                throw new ArgumentNullException("worldId");

            this.WorldId = worldId;
        }
    }
}
