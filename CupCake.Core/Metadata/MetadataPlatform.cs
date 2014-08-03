using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MuffinFramework.Platforms;

namespace CupCake.Core.Metadata
{
    public class MetadataPlatform : Platform
    {
        private readonly ConcurrentDictionary<object, MetadataStore> _metadataStores = new ConcurrentDictionary<object, MetadataStore>();

        protected override void Enable()
        {
        }

        public MetadataStore this[object key]
        {
            get { return _metadataStores.GetOrAdd(key, o => new MetadataStore()); }
        }

        public bool Remove(object key)
        {
            MetadataStore value;
            return _metadataStores.TryRemove(key, out value);
        }
    }
}
