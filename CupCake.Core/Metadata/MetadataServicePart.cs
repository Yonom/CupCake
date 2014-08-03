using System;

namespace CupCake.Core.Metadata
{
    public abstract class MetadataServicePart<T> : CupCakeServicePart<T>
    {
        protected abstract object MetadataKey { get; }
        private readonly Lazy<MetadataStore> _metadataStore;

        protected MetadataStore MetadataStore
        {
            get { return this._metadataStore.Value; }
        }

        protected MetadataServicePart()
        {
            this._metadataStore = new Lazy<MetadataStore>(() => this.MetadataPlatform[this.MetadataKey]);
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
