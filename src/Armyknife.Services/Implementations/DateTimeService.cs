using Armyknife.Services.Interfaces;
using System;

namespace Armyknife.Services.Implementations
{
   internal class DateTimeService : IDateTimeService
   {
      public DateTime Now => DateTime.Now;
   }
}
