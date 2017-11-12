using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;
using System.Web;

namespace Armyknife.Tools.Implementations
{
   internal class HtmldecodeTool : ISynchronousTool
   {
      public string Name => "htmldecode";

      public string Description => ToolResources.HtmldecodeDescription;

      public string Category => CategoryResources.WebCategory;

      public string HelpText => ToolResources.HtmldecodeHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         return HttpUtility.HtmlDecode(input);
      }
   }
}
