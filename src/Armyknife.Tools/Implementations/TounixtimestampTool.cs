using System;
using System.Collections.Generic;
using System.Globalization;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;

namespace Armyknife.Tools.Implementations
{
   internal class TounixtimestampTool : ISynchronousTool
   {
      public string Name => "tounixtimestamp";

      public string Description => ToolResources.TounixtimestampDescription;

      public string Category => CategoryResources.DateCategory;

      public string HelpText => ToolResources.TounixtimestampHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         DateTime inputDateTime;
         string input = args.GetValue(Constants.InputKey);
         if (string.IsNullOrEmpty(input))
         {
            inputDateTime = DateTime.Now;
         }
         else
         {
            if (!DateTime.TryParse(input, null, DateTimeStyles.RoundtripKind, out inputDateTime))
            {
               throw new ArmyknifeException($"Input '{input}' is not a valid ISO 8601 date and time string (example: 2017-08-20T15:00:00Z or 2017-08-20T15:00:00+02:00).");
            }
         }

         var baseline = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
         double difference = (inputDateTime.ToUniversalTime() - baseline).TotalMilliseconds;

         return difference.ToString("0");
      }
   }
}
