using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Core.Storage
{
    public interface IStorageProvider
    {
        void Set(string id, string key, string value);
        string Get(string id, string key);
    }
}
