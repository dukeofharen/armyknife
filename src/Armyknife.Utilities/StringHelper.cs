using System;
using System.Linq;

namespace Armyknife.Utilities
{
   public static class StringHelper
   {
      public static string GetFileExtension(this string input)
      {
         var parts = input.Split('.');
         return parts.Last();
      }

      //https://stackoverflow.com/questions/7170909/trim-string-from-the-end-of-a-string-in-net-why-is-this-missing
      public static string TrimEnd(this string input, string suffixToRemove, StringComparison comparisonType)
      {
         if (input != null && suffixToRemove != null && input.EndsWith(suffixToRemove, comparisonType))
         {
            return input.Substring(0, input.Length - suffixToRemove.Length);
         }

         return input;
      }
   }
}
