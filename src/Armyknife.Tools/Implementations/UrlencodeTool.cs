using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;
using System.Web;

namespace Armyknife.Tools.Implementations
{
   internal class UrlencodeTool : ISynchronousTool
   {
      public string Name => "urlencode";

      public string Description => ToolResources.UrlencodeDescription;

      public string Category => CategoryResources.WebCategory;

      public string HelpText => ToolResources.UrlencodeHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         return HttpUtility.UrlEncode(input);
      }
   }
}
