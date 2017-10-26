using System.Collections.Generic;

namespace Armyknife.Utilities
{
    public static class DictionaryHelper
    {
        public static string GetValue(this IDictionary<string, string> dictionary, string key)
        {
            dictionary.TryGetValue(key, out string value);
            return value;
        }
    }
}
