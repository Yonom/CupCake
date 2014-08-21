using System;

namespace CupCake.Core.Metadata
{
    public abstract class MetadataServicePart<T> : CupCakeServicePart<T>
    {
        private readonly Lazy<MetadataStore> _metadataStore;

        protected MetadataServicePart()
        {
            this._metadataStore = new Lazy<MetadataStore>(() =>
            {
                MetadataStore value = this.MetadataPlatform[this.MetadataKey];
                value.MetadataChanged += this.MetadataChanged;
                return value;
            });
        }

        protected abstract object MetadataKey { get; }

        protected MetadataStore MetadataStore
        {
            get { return this._metadataStore.Value; }
        }

        public event EventHandler<MetadataChangedEventArgs> MetadataChanged;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.MetadataPlatform.Remove(this.MetadataKey);
            }

            base.Dispose(disposing);
        }

        public TMetadata Get<TMetadata>(string id)
        {
            TMetadata value;
            this.MetadataStore.GetMetadata(id, out value);
            return value;
        }

        public void Set<TMetadata>(string id, TMetadata value)
        {
            this.MetadataStore.SetMetadata(id, value);
        }
    }
}