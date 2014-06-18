using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using CupCake.Core;
using MuffinFramework;
using PlayerIOClient;

namespace CupCake.Host
{
    public class CupCakeClient : MuffinClient
    {
        private Connection _connection;

        public CupCakeClient(params ComposablePartCatalog[] catalog)
            : base(catalog)
        {

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            this.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory,
                "CupCake.*.dll"));
            this.PlatformLoader.EnableComplete += this.PlatformLoader_EnableComplete;
        }

        public void Start(Connection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            this._connection = connection;

            base.Start();
        }
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