using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using CupCake.Core;
using JetBrains.Annotations;
using MuffinFramework;
using PlayerIOClient;

namespace CupCake.Host
{
    /// <summary>
    ///     Class CupCakeClient.
    ///     Use this client to host CupCake in your own application.
    /// </summary>
    public class CupCakeClient : MuffinClient
    {
        private CupCakeClientArgs _args;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CupCakeClient" /> class.
        ///     Automatically adds the current assembly and every assembly with the format "CupCake.*.dll".
        /// </summary>
        /// <param name="catalog">The ComposablePartCatalog array that contains zero or more items to add to the catalog.</param>
        public CupCakeClient(params ComposablePartCatalog[] catalog)
            : base(catalog)
        {
            
            this.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory, "CupCake.*.dll"));
            this.PlatformLoader.EnableComplete += this.PlatformLoader_EnableComplete;
        }

        // TODO: Remove in v1.8
        /// <summary>
        ///     Starts CupCake and sets the connection to the given one.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <exception cref="System.ArgumentNullException">connection</exception>
        [Obsolete("Use the overload with CupCakeClientArgs parameter.")]
        public void Start([NotNull]Connection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            this._args = new CupCakeClientArgs(connection);
            base.Start();
        }

        /// <summary>
        /// Starts CupCake and sets the connection to the given one.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="System.ArgumentNullException">connection</exception>
        public void Start([NotNull]CupCakeClientArgs args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            this._args = args;
            base.Start();
        }

        /// <summary>
        ///     Starts the client.
        ///     Obsolete. Use the overload with Connection parameter.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Please provide the connection parameter</exception>
        [Obsolete("Use the overload with CupCakeClientArgs parameter.", true)]
#pragma warning disable 809
        public override void Start()
#pragma warning restore 809
        {
            throw new NotSupportedException("Please provide the connection parameter");
        }

        private void PlatformLoader_EnableComplete(object sender, EventArgs e)
        {
            var connectionPlatform = this.PlatformLoader.Get<ConnectionPlatform>();
            connectionPlatform.Connection = this._args.Connection;
            connectionPlatform.WorldId = this._args.WorldId;
        }
    }
}