using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using CupCake.Core;
using MuffinFramework;
using PlayerIOClient;

namespace CupCake.Host
{
    /// <summary>
    /// Class CupCakeClient.
    /// Use this client to host CupCake in your own application.
    /// </summary>
    public class CupCakeClient : MuffinClient
    {
        private Connection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CupCakeClient"/> class.
        /// Automatically adds the current assembly and every assembly with the format "CupCake.*.dll".
        /// </summary>
        /// <param name="catalog">The ComposablePartCatalog array that contains zero or more items to add to the catalog.</param>
        public CupCakeClient(params ComposablePartCatalog[] catalog)
            : base(catalog)
        {
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            this.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory,
                "CupCake.*.dll"));
            this.PlatformLoader.EnableComplete += this.PlatformLoader_EnableComplete;
        }

        /// <summary>
        /// Starts CupCake and sets the connection to the given one.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <exception cref="System.ArgumentNullException">connection</exception>
        public void Start(Connection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            this._connection = connection;

            base.Start();
        }

        /// <summary>
        /// Starts the client.
        /// Obsolete. Use the overload with Connection parameter.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Please provide the connection parameter</exception>
        [Obsolete("Use the overload with Connection parameter.", true)]
#pragma warning disable 809
        public override void Start()
#pragma warning restore 809
        {
            throw new NotSupportedException("Please provide the connection parameter");
        }

        private void PlatformLoader_EnableComplete(object sender, EventArgs e)
        {
            this.PlatformLoader.Get<ConnectionPlatform>().Connection = this._connection;
        }
    }
}