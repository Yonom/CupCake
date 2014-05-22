using System.Collections.Generic;

namespace CupCake.Players.Metadata
{
    public class MetadataManager
    {
        private readonly Dictionary<string, object> _metadatas = new Dictionary<string, object>();

        public TMetadata GetMetadata<TMetadata>(string metadataId)
        {
            lock (this._metadatas)
            {
                if (_metadatas.ContainsKey(metadataId))
                    return (TMetadata)this._metadatas[metadataId];

                return default(TMetadata);
            }
        }

        public void SetMetadata<TMetaData>(string metadataId, TMetaData value)
        {
            lock (this._metadatas)
            {
                _metadatas[metadataId] = value;
            }
        }
    }
}
