﻿using System.Collections.Generic;

namespace CupCake.Storage
{
    public class BasicStorageProvider : IStorageProvider
    {
        private readonly Dictionary<KeyValuePair<string, string>, string> _kvStore =
            new Dictionary<KeyValuePair<string, string>, string>();

        public void Set(string id, string key, string value)
        {
            this._kvStore[new KeyValuePair<string, string>(id, key)] = value;
        }

        public string Get(string id, string key)
        {
            var query = new KeyValuePair<string, string>(id, key);
            if (this._kvStore.ContainsKey(query))
                return this._kvStore[query];

            return null;
        }

        public bool Delete(string id, string key)
        {
            var query = new KeyValuePair<string, string>(id, key);
            return this._kvStore.Remove(query);
        }
    }
}