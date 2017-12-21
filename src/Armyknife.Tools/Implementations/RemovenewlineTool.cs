using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;

namespace Armyknife.Tools.Implementations
{
   internal class RemovenewlineTool : ISynchronousTool
   {
      public string Name => "removenewline";

      public string Description => ToolResources.RemovenewlineDescription;

      public string Category => CategoryResources.TextCategory;

      public string HelpText => ToolResources.RemovenewlineHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         string result = input
            .Replace("\r\n", string.Empty)
            .Replace("\n", string.Empty);
         return result;
      }
   }
}
