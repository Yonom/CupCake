using System;

namespace CupCake.Core.Metadata
{
    public abstract class MetadataProvider
    {
        protected readonly MetadataPlatform MetadataPlatform;
        private MetadataStore _metadataStore;

        protected MetadataProvider(MetadataPlatform metadataPlatform)
        {
            this.MetadataPlatform = metadataPlatform;
        }

        protected abstract object MetadataKey { get; }

        protected MetadataStore MetadataStore
        {
            get
            {
                if (this._metadataStore == null)
                {
                    this._metadataStore = this.MetadataPlatform[this.MetadataKey];
                    this._metadataStore.MetadataChanged += this.MetadataChanged;
                }
                return this._metadataStore;
            }
        }

        public event EventHandler<MetadataChangedEventArgs> MetadataChanged;

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