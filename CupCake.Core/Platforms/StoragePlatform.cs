using CupCake.Core.Storage;
using MuffinFramework.Platforms;

namespace CupCake.Core.Platforms
{
    public class StoragePlatform : Platform, IStorageProvider
    {
        public IStorageProvider StorageProvider { get; set; }

        protected override void Enable()
        {
        }

        public void Set(string id, string key, string value)
        {
            this.StorageProvider.Set(id, key, value);
        }

        public string Get(string id, string key)
        {
            return this.StorageProvider.Get(id, key);
        }
    }
}