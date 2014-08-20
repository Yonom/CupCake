using System;

namespace CupCake.Core.Metadata
{
    public abstract class MetadataServicePart<T> : CupCakeServicePart<T>
    {
        protected abstract object MetadataKey { get; }
        private readonly Lazy<MetadataStore> _metadataStore;

        public event EventHandler<MetadataChangedEventArgs> MetadataChanged;

        protected MetadataStore MetadataStore
        {
            get { return this._metadataStore.Value; }
        }

        protected MetadataServicePart()
        {
            this._metadataStore = new Lazy<MetadataStore>(() =>
            {
                var value = this.MetadataPlatform[this.MetadataKey];
                value.MetadataChanged += this.MetadataChanged;
                return value;
            });
        }

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
