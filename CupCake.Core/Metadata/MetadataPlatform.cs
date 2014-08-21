using System.Collections.Concurrent;
using MuffinFramework.Platforms;

namespace CupCake.Core.Metadata
{
    public class MetadataPlatform : Platform
    {
        private readonly ConcurrentDictionary<object, MetadataStore> _metadataStores =
            new ConcurrentDictionary<object, MetadataStore>();

        public MetadataStore this[object key]
        {
            get { return this._metadataStores.GetOrAdd(key, o => new MetadataStore()); }
        }

        protected override void Enable()
        {
        }

        public bool Remove(object key)
        {
            MetadataStore value;
            return this._metadataStores.TryRemove(key, out value);
        }
    }
}