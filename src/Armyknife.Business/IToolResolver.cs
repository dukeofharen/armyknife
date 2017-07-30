using System.Collections.Generic;
using Armyknife.Business.Tools;

namespace Armyknife.Business
{
    public interface IToolResolver
    {
        ITool ResolveTool(string name);

        IEnumerable<string> GetToolNames();
    }
}
