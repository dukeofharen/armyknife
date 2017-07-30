using System.Collections.Generic;
using System.Linq;
using Armyknife.Models;
using Armyknife.Services;

namespace Armyknife.Business.Implementations
{
    internal class InputReader : IInputReader
    {
        private readonly IFileService _fileService;

        public InputReader(IFileService fileService)
        {
            _fileService = fileService;
        }

        public string GetInput(string[] args, IDictionary<string, string> argsDictionary)
        {
            string result = string.Empty;

            // If no further arguments have been passed on the command line, assume the other command line arguments are the input.
            if (argsDictionary.Count == 0)
            {
                result = string.Join(" ", args.Skip(1));
            }
            else if (argsDictionary.ContainsKey(Constants.FileInputKey))
            {
                result = _fileService.ReadAllText(argsDictionary[Constants.FileInputKey]);
            }

            return result;
        }
    }
}
