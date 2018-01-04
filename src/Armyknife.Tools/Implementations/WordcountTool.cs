using System;
using System.Collections.Generic;
using System.Linq;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;

namespace Armyknife.Tools.Implementations
{
   internal class WordcountTool : ISynchronousTool
   {
      public string Name => "wordcount";

      public string Description => ToolResources.WordcountDescription;

      public string Category => CategoryResources.TextCategory;

      public string HelpText => ToolResources.WordcountHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         if (!args.ContainsKey(Constants.InputKey))
         {
            throw new ArmyknifeException(ExceptionResources.NoInput);
         }

         string input = args.GetValue(Constants.InputKey);
         string result = input.Split(new[] {"\n", "\r\n"}, StringSplitOptions.None)
            .SelectMany(p => p.Split(' '))
            .Count()
            .ToString();

         return result;
      }
   }
}
