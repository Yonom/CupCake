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
                if (this._metadatas.ContainsKey(metadataId))
                    return (TMetadata)this._metadatas[metadataId];

                return default(TMetadata);
            }
        }

        public void SetMetadata<TMetaData>(string metadataId, TMetaData value)
        {
            lock (this._metadatas)
            {
                this._metadatas[metadataId] = value;
            }
        }
    }
}