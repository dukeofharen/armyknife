using System;
using System.Collections.Generic;

namespace Armyknife.Services.Interfaces
{
   public interface ILogger
   {
      void Log(object source, string message);

      void Log(object source, Exception exception);

      void Log(object source, IDictionary<string, string> arguments);

      List<string> GetLogMessages();
   }
}
