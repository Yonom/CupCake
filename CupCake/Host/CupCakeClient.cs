using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using CupCake.Core.Platforms;
using MuffinFramework;
using PlayerIOClient;

namespace CupCake.Host
{
    public class CupCakeClient : MuffinClient
    {
        private readonly Connection _connection;

        public CupCakeClient(Connection connection, params ComposablePartCatalog[] catalog)
            : base(catalog)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            this._connection = connection;

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            this.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, "CupCake.*.dll"));
            this.PlatformLoader.EnableComplete += this.PlatformLoader_EnableComplete;
        }

        private void PlatformLoader_EnableComplete(object sender, EventArgs e)
        {
            this.PlatformLoader.Get<ConnectionPlatform>().Connection = this._connection;
        }
    }
}