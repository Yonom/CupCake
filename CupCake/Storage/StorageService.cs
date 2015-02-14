namespace CupCake.Storage
{
    public class StorageService : Service, IStorageProvider
    {
        public IStorageProvider StorageProvider { get; set; } // TODO: Disallow set

        public StorageService()
        {
            this.StorageProvider = new BasicStorageProvider();
        }
        public void Set(string id, string key, string value)
        {
            this.StorageProvider.Set(id, key, value);
        }

        public string Get(string id, string key)
        {
            return this.StorageProvider.Get(id, key);
        }

        public bool Delete(string id, string key)
        {
            return this.StorageProvider.Delete(id, key);
        }
    }
}