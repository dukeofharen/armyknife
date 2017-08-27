using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services;
using System.Collections.Generic;

namespace Armyknife.Business.Tools.Implementations
{
    public class ToMimeTool : ITool
    {
        private readonly IMimeService _mimeService;

        public ToMimeTool(IMimeService mimeService)
        {
            _mimeService = mimeService;
        }

        public string Name => "tomime";

        public string Description => ToolResources.ToMimeDescription;

        public string Category => CategoryResources.TextCategory;

        public string HelpText => ToolResources.ToMimeHelp;

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
