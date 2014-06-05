using CupCake.Core.Storage;
using MuffinFramework.Platforms;

namespace CupCake.Core
{
    public class StoragePlatform : Platform, IStorageProvider
    {
        public IStorageProvider StorageProvider { get; set; }

        public void Set(string id, string key, string value)
        {
            this.StorageProvider.Set(id, key, value);
        }

        public string Get(string id, string key)
        {
            return this.StorageProvider.Get(id, key);
        }

        protected override void Enable()
        {
        }
    }
}