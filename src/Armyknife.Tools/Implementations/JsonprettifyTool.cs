using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace Armyknife.Tools.Implementations
{
   internal class JsonprettifyTool : ISynchronousTool
   {
      private const string CharacterKey = "character";
      private const string TabSizeKey = "tabsize";

      public string Name => "jsonprettify";

      public string Description => ToolResources.JsonprettifyDescription;

      public string Category => CategoryResources.WebCategory;

      public string HelpText => ToolResources.JsonprettifyHelp;

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
            textWriter.Formatting = Formatting.Indented;
            textWriter.Indentation = args.GetValue(TabSizeKey, 3);
            textWriter.IndentChar = args.GetValue(CharacterKey) == "tab" ? '\t' : ' ';

            textWriter.WriteToken(textReader);

            string formattedJson = stringWriter.ToString();
            return formattedJson;
         }
      }
   }
}
