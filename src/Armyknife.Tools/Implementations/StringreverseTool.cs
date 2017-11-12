using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;
using System.Linq;

namespace Armyknife.Tools.Implementations
{
   internal class StringreverseTool : ISynchronousTool
   {
      public string Name => "stringreverse";

      public string Description => ToolResources.StringreverseDescription;

      public string Category => CategoryResources.TextCategory;

      public string HelpText => ToolResources.StringreverseHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         return new string(input.ToCharArray().Reverse().ToArray());
      }
   }
}
