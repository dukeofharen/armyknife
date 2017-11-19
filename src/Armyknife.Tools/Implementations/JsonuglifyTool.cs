using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Armyknife.Tools.Implementations
{
   internal class JsonuglifyTool : ISynchronousTool
   {
      public string Name => "jsonuglify";

      public string Description => ToolResources.JsonuglifyDescription;

      public string Category => CategoryResources.WebCategory;

      public string HelpText => ToolResources.JsonuglifyHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);

         var builder = new StringBuilder();

         using (var stringReader = new StringReader(input))
         using (var stringWriter = new StringWriter(builder))
         using (var textWriter = new JsonTextWriter(stringWriter))
         using (var textReader = new JsonTextReader(stringReader))
         {
            textWriter.Formatting = Formatting.None;
            textWriter.Indentation = 0;

            textWriter.WriteToken(textReader);

            string formattedJson = stringWriter.ToString();
            return formattedJson;
         }
      }
   }
}
