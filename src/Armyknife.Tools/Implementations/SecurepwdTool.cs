using System;
using System.Collections.Generic;
using System.Text;
using Armyknife.Exceptions;
using Armyknife.Resources;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Utilities;

namespace Armyknife.Tools.Implementations
{
   internal class SecurepwdTool : ISynchronousTool
   {
      private static readonly Random Random = new Random();
      private readonly string[] _randomChars = {
         "ABCDEFGHJKLMNOPQRSTUVWXYZ",
         "abcdefghijkmnopqrstuvwxyz", 
         "0123456789",
         "!@$?_-#.$()*&^%,=+|"
      };

      public string Name => "securepwd";

      public string Description => ToolResources.SecurepwdDescription;

      public string Category => CategoryResources.HashingCategory;

      public string HelpText => ToolResources.SecurepwdHelp;

      public bool ShowToolInHelp => true;

      public string Execute(IDictionary<string, string> args)
      {
         int length = args.GetValue("length", 20);
         bool useCapitals = args.GetValue("capitals", true);
         bool useLowercase = args.GetValue("lowercase", true);
         bool useNumbers = args.GetValue("numbers", true);
         bool useSpecialChars = args.GetValue("specialchars", true);

         var charIndexes = new List<int>();
         if (useCapitals)
         {
            charIndexes.Add(0);
         }

         if (useLowercase)
         {
            charIndexes.Add(1);
         }

         if (useNumbers)
         {
            charIndexes.Add(2);
         }

         if (useSpecialChars)
         {
            charIndexes.Add(3);
         }

         if (charIndexes.Count == 0)
         {
            throw new ArmyknifeException("Allow at least one type of character.");
         }

         var builder = new StringBuilder();
         for (int i = 0; i < length; i++)
         {
            int specialCharArrayIndex = charIndexes[Random.Next(0, charIndexes.Count)];
            var charArray = _randomChars[specialCharArrayIndex];
            builder.Append(charArray[Random.Next(0, charArray.Length)]);
         }

         return builder.ToString();
      }
   }
}
