namespace CupCake.Core.Storage
{
    public interface IStorageProvider
    {
        void Set(string id, string key, string value);
        string Get(string id, string key);
    }
}