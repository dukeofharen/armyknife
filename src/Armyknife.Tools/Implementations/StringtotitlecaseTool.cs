using System.Collections.Generic;
using System.Globalization;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;

namespace Armyknife.Tools.Implementations
{
   internal class StringtotitlecaseTool : ISynchronousTool
   {
      public string Name => "stringtotitlecase";

      public string Description => ToolResources.StringtotitlecaseDescription;

      public string Category => CategoryResources.TextCategory;

      public string HelpText => ToolResources.StringtotitlecaseHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         var textInfo = CultureInfo.CurrentCulture.TextInfo;
         string result = textInfo.ToTitleCase(input);

         return result;
      }
   }
}
