using Armyknife.Business.Interfaces;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using System.Collections.Generic;

namespace Armyknife.Tools.Implementations
{
    internal class ToMimeTool : ISynchronousTool
    {
        private readonly IMimeService _mimeService;

        public ToMimeTool(IMimeService mimeService)
        {
            _mimeService = mimeService;
        }

        public string Name => "tomime";

        public string Description => ToolResources.ToMimeDescription;

        public string Category => CategoryResources.FileCategory;

        public string HelpText => ToolResources.ToMimeHelp;

        public bool ShowToolInHelp => true;

        public string Execute(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(Constants.InputKey))
            {
                throw new ArmyknifeException(ExceptionResources.NoInput);
            }

            string input = args[Constants.InputKey];
            string mime = _mimeService.GetMimeType(input);
            return mime;
        }
    }
}
