using System.Collections.Generic;
using Armyknife.Business.Tools;
using Armyknife.Models;

namespace Armyknife.Business
{
    public interface IToolResolver
    {
        ITool ResolveTool(string name);

        IEnumerable<ToolMetaDataModel> GetToolMetData();
    }
}
