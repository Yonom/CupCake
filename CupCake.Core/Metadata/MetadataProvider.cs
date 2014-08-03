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
        
        protected MetadataStore MetadataStore
        {
            get
            {
                if (_metadataStore == null)
                    _metadataStore = this.MetadataPlatform[this.MetadataKey];
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

        public bool GetBool(string id)
        {
            return this.Get<bool>(id);
        }

        public int GetInt(string id)
        {
            return this.Get<int>(id);
        }

        public uint GetUInt(string id)
        {
            return this.Get<uint>(id);
        }

        public short GetShort(string id)
        {
            return this.Get<short>(id);
        }

        public ushort GetUShort(string id)
        {
            return this.Get<ushort>(id);
        }

        public long GetLong(string id)
        {
            return this.Get<long>(id);
        }

        public ulong GetULong(string id)
        {
            return this.Get<ulong>(id);
        }

        public byte GetByte(string id)
        {
            return this.Get<byte>(id);
        }

        public sbyte GetSByte(string id)
        {
            return this.Get<sbyte>(id);
        }

        public char GetChar(string id)
        {
            return this.Get<char>(id);
        }

        public string GetString(string id)
        {
            return this.Get<string>(id);
        }

        public double GetDouble(string id)
        {
            return this.Get<double>(id);
        }

        public float GetFloat(string id)
        {
            return this.Get<float>(id);
        }

        public decimal GetDecimal(string id)
        {
            return this.Get<decimal>(id);
        }
    }
}
