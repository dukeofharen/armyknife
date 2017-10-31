using System.Collections.Generic;
using Armyknife.Models;

namespace Armyknife.Business.Interfaces
{
    public interface IToolResolver
    {
        ITool ResolveTool(string name);

        IEnumerable<ToolMetaDataModel> GetToolMetData();
    }
}
