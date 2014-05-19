using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using CupCake.Core.Platforms;
using MuffinFramework;
using PlayerIOClient;

namespace CupCake.Core.Host
{
    public class CupCakeClient : MuffinClient
    {
        private readonly Connection _connection;

        public CupCakeClient(Connection connection)
            : base(new ComposablePartCatalog[0])
        {
            this._connection = connection;

            this.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
            this.PlatformLoader.EnableComplete += this.PlatformLoader_EnableComplete;
        }

        private void PlatformLoader_EnableComplete(object sender, EventArgs e)
        {
            this.PlatformLoader.Get<ConnectionPlatform>().Connection = this._connection;
        }
    }
}