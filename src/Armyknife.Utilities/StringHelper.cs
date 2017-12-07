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
   }
}
