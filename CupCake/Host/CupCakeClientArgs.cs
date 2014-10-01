using System;
using JetBrains.Annotations;
using PlayerIOClient;

namespace CupCake.Host
{
    /// <summary>
    ///     Class CupCakeClientArgs
    /// </summary>
    public class CupCakeClientArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CupCakeClientArgs" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="worldId">The world identifier.</param>
        public CupCakeClientArgs([NotNull] Connection connection, [NotNull] string worldId)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (worldId == null)
                throw new ArgumentNullException("worldId");

            this.Connection = connection;
            this.WorldId = worldId;
        }

        /// <summary>
        ///     Gets or sets the connection.
        /// </summary>
        /// <value>
        ///     The connection.
        /// </value>
        public Connection Connection { get; set; }

        /// <summary>
        ///     Gets the world identifier.
        /// </summary>
        /// <value>
        ///     The world identifier.
        /// </value>
        public string WorldId { get; private set; }
    }
}