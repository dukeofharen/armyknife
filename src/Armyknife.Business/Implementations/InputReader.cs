using System.Collections.Generic;
using System.Linq;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;
using System;
using Armyknife.Utilities;

namespace Armyknife.Business.Implementations
{
   internal class InputReader : IInputReader
   {
      private readonly IConsoleService _consoleService;

      public InputReader(IConsoleService consoleService)
      {
         _consoleService = consoleService;
      }

      public string GetInput(string[] args, IDictionary<string, string> argsDictionary)
      {
         string result = string.Empty;

         string pipedData = _consoleService.ReadPipedData();
         if (!string.IsNullOrWhiteSpace(pipedData))
         {
            // If piped data was passed to the application, use this.
            result = pipedData;
            result = result.TrimEnd("\r\n", StringComparison.OrdinalIgnoreCase);
            result = result.TrimEnd("\n", StringComparison.OrdinalIgnoreCase);
         }
         else if (argsDictionary.Count == 0)
         {
            // If no further arguments have been passed on the command line, assume the other command line arguments are the input.
            result = string.Join(" ", args.Skip(1));
         }

         return result;
      }
   }
}
