using System.Collections.Concurrent;

namespace CupCake.Players.Metadata
{
    public class MetadataManager
    {
        private readonly ConcurrentDictionary<string, object> _metadatas = new ConcurrentDictionary<string, object>();

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
        }
    }
}