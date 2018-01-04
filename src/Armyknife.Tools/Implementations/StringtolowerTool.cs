using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;

namespace Armyknife.Tools.Implementations
{
   internal class StringtolowerTool : ISynchronousTool
   {
      public string Name => "stringtolower";

      public string Description => ToolResources.StringtolowerDescription;

      public string Category => CategoryResources.TextCategory;

      public string HelpText => ToolResources.StringtolowerHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         string result = input.ToLower();
         return result;
      }
   }
}
