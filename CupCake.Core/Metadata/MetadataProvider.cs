using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Core.Metadata
{
    public abstract class MetadataProvider
    {
        protected readonly MetadataPlatform MetadataPlatform;
        protected abstract object MetadataKey { get; }
        private MetadataStore _metadataStore;

        public event EventHandler<MetadataChangedEventArgs> MetadataChanged;
        
        protected MetadataStore MetadataStore
        {
            get
            {
                if (_metadataStore == null)
                {
                    _metadataStore = this.MetadataPlatform[this.MetadataKey];
                    _metadataStore.MetadataChanged += this.MetadataChanged;
                }
                return this._metadataStore;
            }
        }

        protected MetadataProvider(MetadataPlatform metadataPlatform)
        {
            this.MetadataPlatform = metadataPlatform;
        }

        public T Get<T>(string id)
        {
            T value;
            this.MetadataStore.GetMetadata(id, out value);
            return value;
        }

        public void Set<T>(string id, T value)
        {
            this.MetadataStore.SetMetadata(id, value);
        }
    }
}
