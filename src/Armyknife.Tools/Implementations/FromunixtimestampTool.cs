using System;
using System.Collections.Generic;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;

namespace Armyknife.Tools.Implementations
{
   internal class FromunixtimestampTool : ISynchronousTool
   {
      public string Name => "fromunixtimestamp";

      public string Description => ToolResources.FromunixtimestampDescription;

      public string Category => CategoryResources.DateCategory;

      public string HelpText => ToolResources.FromunixtimestampHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args[Constants.InputKey];
         if (!long.TryParse(input, out long unixTimestamp))
         {
            throw new ArmyknifeException($"Input string '{input}' is not a valid UNIX timestamp.");
         }

         if (input.Length != 10 && input.Length != 13)
         {
            throw new ArmyknifeException("The length of the UNIX timestamp should be either 10 or 13 characters long.");
         }

         DateTime result = DateTime.MinValue;
         var baseline = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
         if (input.Length == 10)
         {
            result = baseline.AddSeconds(unixTimestamp);
         }

         if (input.Length == 13)
         {
            result = baseline.AddMilliseconds(unixTimestamp);
         }

         return result.ToString("yyyy-MM-dd HH:mm:ss:fff");
      }
   }
}
