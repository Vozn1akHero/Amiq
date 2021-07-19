using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.Utils
{
    public abstract class CacheFacade
    {
        public static Dictionary<string, SynchronizedCache> Caches 
            = new Dictionary<string, SynchronizedCache>();

        public static SynchronizedCache GetCacheByKey(string key)
        {
            if (Caches.ContainsKey(key)) return Caches[key];
            var cache = new KeyValuePair<string, SynchronizedCache>(key, new SynchronizedCache());
            Caches.Append(cache);
            return cache.Value;
        }
    }
}
