using System.Collections.Generic;
using System.IO;
using Armyknife.Models;
using Armyknife.Services;

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

        public void WriteOutput(string result, IDictionary<string, string> argsDictionary)
        {
            if (argsDictionary.ContainsKey(Constants.FileOutputKey))
            {
                string path = argsDictionary[Constants.FileOutputKey];
                if (!path.Contains("/") && !path.Contains(@"\"))
                {
                    // We know this is not a full path so we prepend the current working directory to it.
                    path = Path.Combine(_consoleService.GetConsolePath(), path);
                }

                _fileService.WriteAllText(path, result);
            }
            else
            {
                _consoleService.WriteLine(result);
            }
        }
    }
}
