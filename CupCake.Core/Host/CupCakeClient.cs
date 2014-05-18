using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using MuffinFramework;

namespace CupCake.Core.Host
{
    public class CupCakeClient : MuffinClient
    {
        public CupCakeClient()
            : base(new ComposablePartCatalog[0])
        {
            this.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
        }
    }
}