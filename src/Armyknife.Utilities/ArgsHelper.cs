using System.Collections.Generic;
using System.Linq;

namespace Armyknife.Utilities
{
    public static class ArgsHelper
    {
        public static IDictionary<string, string> Parse(this string[] args)
        {
            var subResult = new Dictionary<string, List<string>>();

            string varPointer = string.Empty;
            foreach (var arg in args)
            {
                if (arg.StartsWith("--"))
                {
                    varPointer = arg.Replace("--", string.Empty);
                    subResult.Add(varPointer, new List<string>());
                }
                else
                {
                    if (subResult.ContainsKey(varPointer))
                    {
                        subResult[varPointer].Add(arg);
                    }
                }
            }

            return subResult
                .ToDictionary(d => d.Key, d => string.Join(" ", d.Value));
        }
    }
}
