using Armyknife.Business.Interfaces;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services;
using System.Collections.Generic;
using System.Linq;

namespace Armyknife.Business.Tools.Implementations
{
    internal class FromMimeTool : ISynchronousTool
    {
        private readonly IMimeService _mimeService;

        public FromMimeTool(IMimeService mimeService)
        {
            _mimeService = mimeService;
        }

        public string Name => "frommime";

        public string Description => ToolResources.FromMimeDescription;

        public string Category => CategoryResources.FileCategory;

        public string HelpText => ToolResources.FromMimeHelp;

        public string Execute(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(Constants.InputKey))
            {
                throw new ArmyknifeException(ExceptionResources.NoInput);
            }

            string input = args[Constants.InputKey];
            var extensions = _mimeService.GetExtensions(input).ToArray();
            if(!extensions.Any())
            {
                throw new ArmyknifeException(string.Format(ExceptionResources.FromMimeNoResult, input));
            }

            string result = string.Join(", ", extensions);
            return result;
        }
    }
}
