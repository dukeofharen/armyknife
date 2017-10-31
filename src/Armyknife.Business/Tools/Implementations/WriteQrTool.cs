using Armyknife.Business.Interfaces;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using Armyknife.Utilities;
using System.Collections.Generic;

namespace Armyknife.Business.Tools.Implementations
{
    internal class WriteQrTool : ISynchronousTool
    {
        private const string WidthKey = "width";
        private const string HeightKey = "height";
        private const string OpenFileKey = "openFile";
        private readonly IBarcodeService _barcodeService;
        private readonly IFileService _fileService;
        private readonly IProcessService _processService;

        public WriteQrTool(
            IBarcodeService barcodeService,
            IFileService fileService,
            IProcessService processService)
        {
            _barcodeService = barcodeService;
            _fileService = fileService;
            _processService = processService;
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

            bool openImage = args.GetValue(OpenFileKey, false);
            if (openImage)
            {
                _processService.StartProcess(writeLocation);
            }

            return string.Empty;
        }
    }
}
