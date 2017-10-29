using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services;
using Armyknife.Utilities;
using System.Collections.Generic;

namespace Armyknife.Business.Tools.Implementations
{
    internal class WriteQrTool : ITool
    {
        private const string WidthKey = "width";
        private const string HeightKey = "height";
        private readonly IBarcodeService _barcodeService;
        private readonly IFileService _fileService;

        public WriteQrTool(
            IBarcodeService barcodeService,
            IFileService fileService)
        {
            _barcodeService = barcodeService;
            _fileService = fileService;
        }

        public string Name => "writeqr";

        public string Description => ToolResources.WriteQrDescription;

        public string Category => CategoryResources.ImagingCategory;

        public string HelpText => ToolResources.WriteQrHelp;

        public string Execute(IDictionary<string, string> args)
        {
            if (!args.TryGetValue(Constants.FileOutputKey, out string writeLocation))
            {
                throw new ArmyknifeException("You should provide the 'outputFile'.");
            }

            if (!args.TryGetValue(Constants.InputKey, out string input))
            {
                throw new ArmyknifeException(ExceptionResources.NoInput);
            }

            int width = args.GetValue(WidthKey, 250);
            int height = args.GetValue(HeightKey, 250);
            var qrBytes = _barcodeService.GenerateQrCode(input, height, width);
            _fileService.WriteAllBytes(writeLocation, qrBytes);
            return string.Empty;
        }
    }
}
