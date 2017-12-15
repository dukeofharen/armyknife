using System.Collections.Generic;
using Armyknife.Resources;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;
using System;
using System.Globalization;

namespace Armyknife.Tools.Implementations
{
   internal class WeeknumberTool : ISynchronousTool
   {
      private readonly IDateTimeService _dateTimeService;

      public WeeknumberTool(IDateTimeService dateTimeService)
      {
         _dateTimeService = dateTimeService;
      }

      public string Name => "weeknumber";

      public string Description => ToolResources.WeeknumberDescription;

      public string Category => CategoryResources.DateCategory;

      public string HelpText => ToolResources.WeeknumberHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         // https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date
         var now = _dateTimeService.Now;
         var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(now);
         if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
         {
            now = now.AddDays(3);
         }

         return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString();
      }
   }
}
