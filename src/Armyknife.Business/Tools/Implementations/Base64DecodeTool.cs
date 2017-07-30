using System;
using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;

namespace Armyknife.Business.Tools.Implementations
{
    internal class Base64DecodeTool : ITool
    {
        public string Name => "base64decode";

        public string HelpText => ToolResources.Base64DecodeHelp;

        public byte[] Execute(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(Constants.InputKey))
            {
                throw new ArmyknifeException("No input provided.");
            }

            string input = args[Constants.InputKey];

            return Convert.FromBase64String(input);
        }
    }
}
