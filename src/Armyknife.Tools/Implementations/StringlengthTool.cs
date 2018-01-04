using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;

namespace Armyknife.Tools.Implementations
{
   internal class StringlengthTool : ISynchronousTool
   {
      public string Name => "stringlength";

      public string Description => ToolResources.StringlengthDescription;

      public string Category => CategoryResources.TextCategory;

      public string HelpText => ToolResources.StringlengthHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         string result = input.Length.ToString();

         return result;
      }
   }
}
