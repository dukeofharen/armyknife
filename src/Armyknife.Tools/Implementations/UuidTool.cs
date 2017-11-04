using Armyknife.Business.Interfaces;
using Armyknife.Resources;
using Armyknife.Utilities;
using System;
using System.Collections.Generic;

namespace Armyknife.Tools.Implementations
{
    internal class UuidTool : ISynchronousTool
    {
        private const string HowManyKey = "howmany";
        private const string BracketsKey = "brackets";
        private const string UppercaseKey = "uppercase";
        private const string HyphensKey = "hyphens";

        public string Name => "uuid";

        public string Description => ToolResources.UuidDescription;

        public string Category => CategoryResources.NumberCategory;

        public string HelpText => ToolResources.UuidHelp;

        public bool ShowToolInHelp => true;

        public string Execute(IDictionary<string, string> args)
        {
            var result = new List<string>();
            int howMany = GetHowMany(args);
            for(int i = 0; i < howMany; i++)
            {
                string guid = Guid.NewGuid().ToString();
                if(string.Equals(args.GetValue(BracketsKey), "true", StringComparison.OrdinalIgnoreCase))
                {
                    guid = $"{{{guid}}}";
                }

                if (string.Equals(args.GetValue(UppercaseKey), "true", StringComparison.OrdinalIgnoreCase))
                {
                    guid = guid.ToUpper();
                }

                if (string.Equals(args.GetValue(HyphensKey), "false", StringComparison.OrdinalIgnoreCase))
                {
                    guid = guid.Replace("-", string.Empty);
                }

                result.Add(guid);
            }

            return string.Join(Environment.NewLine, result);
        }

        private static int GetHowMany(IDictionary<string, string> args)
        {
            int howMany = 1;
            if (args.TryGetValue(HowManyKey, out string howManyText))
            {
                int.TryParse(howManyText, out howMany);
            }

            return howMany;
        }
    }
}
