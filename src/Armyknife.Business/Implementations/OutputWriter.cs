using System.Collections.Generic;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;

namespace Armyknife.Business.Implementations
{
    internal class OutputWriter : IOutputWriter
    {
        private readonly IConsoleService _consoleService;
        private readonly IFileService _fileService;

        public OutputWriter(
            IConsoleService consoleService,
            IFileService fileService)
        {
            _consoleService = consoleService;
            _fileService = fileService;
        }

        public void WriteOutput(string result)
        {
            _consoleService.WriteLine(result);
        }
    }
}
