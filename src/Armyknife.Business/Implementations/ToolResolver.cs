using System.Collections.Generic;
using System.Linq;
using Armyknife.Models;
using Armyknife.Business.Interfaces;
using Armyknife.Services.Interfaces;

namespace Armyknife.Business.Implementations
{
   internal class ToolResolver : IToolResolver
   {
      private readonly IServiceContainerWrapper _wrapper;

      public ToolResolver(IServiceContainerWrapper wrapper)
      {
         _wrapper = wrapper;
      }

      public ITool ResolveTool(string name)
      {
         return _wrapper
             .ResolveMultiple<ITool>()
             .Single(t => t.Name == name);
      }

      public IEnumerable<ToolMetaDataModel> GetToolMetData()
      {
         return _wrapper
             .ResolveMultiple<ITool>()
             .Select(t => new ToolMetaDataModel
             {
                Key = t.Name,
                Category = t.Category,
                HelpText = t.HelpText,
                ShortDescription = t.Description,
                ShowToolInHelp = t.ShowToolInHelp
             });
      }
   }
}
