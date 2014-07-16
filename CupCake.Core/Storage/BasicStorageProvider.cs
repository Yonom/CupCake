using System.Collections.Generic;

namespace CupCake.Core.Storage
{
    public class BasicStorageProvider : IStorageProvider
    {
        private readonly Dictionary<KeyValuePair<string, string>, string> _kvStore =
            new Dictionary<KeyValuePair<string, string>, string>();

        public void Set(string id, string key, string value)
        {
            _kvStore[new KeyValuePair<string, string>(id, key)] = value;
        }

        public string Get(string id, string key)
        {
            var query = new KeyValuePair<string, string>(id, key);
            if (_kvStore.ContainsKey(query))
                return _kvStore[query];

            return null;
        }
    }
}