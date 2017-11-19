using Armyknife.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Armyknife.Services.Implementations
{
   internal class Logger : ILogger
   {
      private readonly static List<string> _messages = new List<string>();

      public List<string> GetLogMessages()
      {
         return _messages;
      }

      public void Log(object source, string message)
      {
         string logMessage = $"{source.GetType()}: {message}";
         _messages.Add(logMessage);
      }

      public void Log(object source, Exception exception)
      {
         var builder = new StringBuilder();
         var innerException = exception;
         while(innerException != null)
         {
            builder.AppendLine(innerException.ToString());
            innerException = innerException.InnerException;
         }

         Log(source, builder.ToString());
      }

      public void Log(object source, IDictionary<string, string> arguments)
      {
         var builder = new StringBuilder();
         foreach(var pair in arguments)
         {
            builder.AppendLine($"{pair.Key} = {pair.Value}");
         }

         Log(source, builder.ToString());
      }
   }
}
