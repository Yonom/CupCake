using System;
using System.Collections.Concurrent;

namespace CupCake.Core.Metadata
{
    public class MetadataStore
    {
        private readonly ConcurrentDictionary<string, object> _metadatas = new ConcurrentDictionary<string, object>();

        public event EventHandler<MetadataChangedEventArgs> MetadataChanged;

        protected virtual void OnMetadataChanged(MetadataChangedEventArgs e)
        {
            EventHandler<MetadataChangedEventArgs> handler = this.MetadataChanged;
            if (handler != null) handler(this, e);
        }

        public bool GetMetadata<TMetadata>(string metadataId, out TMetadata metadata)
        {
            object metadataObj;
            bool success = this._metadatas.TryGetValue(metadataId, out metadataObj);

            metadata = default(TMetadata);
            if (metadataObj != null)
                metadata = (TMetadata)metadataObj;

            return success;
        }

        public void SetMetadata<TMetaData>(string metadataId, TMetaData value)
        {
            this._metadatas.AddOrUpdate(metadataId, value, (k, v) => value);
            this.OnMetadataChanged(new MetadataChangedEventArgs(metadataId, value));
        }
    }
}