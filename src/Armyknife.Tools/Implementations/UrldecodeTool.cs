using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using System.Web;
using Armyknife.Utilities;

namespace Armyknife.Tools.Implementations
{
   internal class UrldecodeTool : ISynchronousTool
   {
      public string Name => "urldecode";

      public string Description => ToolResources.UrldecodeDescription;

      public string Category => CategoryResources.WebCategory;

      public string HelpText => ToolResources.UrldecodeHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         return HttpUtility.UrlDecode(input);
      }
   }
}
