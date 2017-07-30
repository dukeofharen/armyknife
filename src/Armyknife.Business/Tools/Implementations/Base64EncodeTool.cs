using System.Collections.Generic;
using System.Text;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;

namespace Armyknife.Business.Tools.Implementations
{
    internal class Base64EncodeTool : ITool
    {
        public string Name => "base64encode";

        public string HelpText => ToolResources.Base64EncodeHelp;

        public byte[] Execute(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(Constants.InputKey))
            {
                throw new ArmyknifeException("No input provided.");
            }

            return Encoding.UTF8.GetBytes(args[Constants.InputKey]);
        }
    }
}
