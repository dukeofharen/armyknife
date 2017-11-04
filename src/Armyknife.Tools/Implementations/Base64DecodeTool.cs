using System;
using System.Collections.Generic;
using System.Text;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Business.Interfaces;

namespace Armyknife.Tools.Implementations
{
    internal class Base64DecodeTool : ISynchronousTool
    {
        public string Name => "base64decode";

        public string Description => ToolResources.Base64DecodeDescription;

        public string Category => CategoryResources.EncodingCategory;

        public string HelpText => ToolResources.Base64DecodeHelp;

        public bool ShowToolInHelp => true;

        public string Execute(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(Constants.InputKey))
            {
                throw new ArmyknifeException(ExceptionResources.NoInput);
            }

            string input = args[Constants.InputKey];

            return Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }
    }
}
