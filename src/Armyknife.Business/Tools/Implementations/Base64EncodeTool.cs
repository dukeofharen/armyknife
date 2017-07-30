using System.Collections.Generic;
using System.Text;
using Armyknife.Models;

namespace Armyknife.Business.Tools.Implementations
{
    internal class Base64EncodeTool : ITool
    {
        public string Name => "base64encode";

        // TODO
        public string HelpText => "TODO";

        public byte[] Execute(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(Constants.InputKey))
            {
                // TODO throw exception
            }

            return Encoding.UTF8.GetBytes(args[Constants.InputKey]);
        }
    }
}
