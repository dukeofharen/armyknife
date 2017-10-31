using Armyknife.Business.Interfaces;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using System.Collections.Generic;

namespace Armyknife.Business.Tools.Implementations
{
    internal class FilextTool : ISynchronousTool
    {
        private readonly IFileExtensionService _fileExtensionService;

        public FilextTool(IFileExtensionService fileExtensionService)
        {
            _fileExtensionService = fileExtensionService;
        }

        public string Name => "filext";

        public string Description => ToolResources.FilextDescription;

        public string Category => CategoryResources.FileCategory;

        public string HelpText => ToolResources.FilextHelp;

        public string Execute(IDictionary<string, string> args)
        {
            if (!args.ContainsKey(Constants.InputKey))
            {
                throw new ArmyknifeException(ExceptionResources.NoInput);
            }

            string input = args[Constants.InputKey];
            var result = _fileExtensionService.GetFileExtensionInfo(input);
            if(result == null)
            {
                throw new ArmyknifeException(string.Format(ExceptionResources.FilextExtensionNotFound, input));
            }

            return string.Format(ToolResources.FilextResult, result.Extension, result.Description, result.UsedBy);
        }
    }
}
