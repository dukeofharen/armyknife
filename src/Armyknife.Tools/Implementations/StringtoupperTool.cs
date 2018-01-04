using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;

namespace Armyknife.Tools.Implementations
{
   internal class StringtoupperTool : ISynchronousTool
   {
      public string Name => "stringtoupper";

      public string Description => ToolResources.StringtoupperDescription;

      public string Category => CategoryResources.TextCategory;

      public string HelpText => ToolResources.StringtoupperHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         string result = input.ToUpper();
         return result;
      }
   }
}
