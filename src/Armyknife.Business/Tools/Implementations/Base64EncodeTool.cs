using System;
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

        public string Description => ToolResources.Base64EncodeDescription;

        public string Category => CategoryResources.TextCategory;

        public string HelpText => ToolResources.Base64EncodeHelp;

        public byte[] Execute(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(Constants.InputKey))
            {
                throw new ArmyknifeException("No input provided.");
            }

            string input = args[Constants.InputKey];

            return Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(input)));
        }
    }
}
